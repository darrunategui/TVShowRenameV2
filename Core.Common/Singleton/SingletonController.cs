using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Threading;

namespace Core.Common.Singleton
{
   public class SingletonController : IDisposable
   {
      private bool _disposed;
      private Mutex _singleInstanceMutex;
      private bool _isFirstInstance;
      private IChannel _ipcChannel;
      private SingletonProxy _proxy;

      /// <summary>
      /// Gets a value indicating whether this instance of the application is the first instance.
      /// </summary>
      public bool IsFirstInstance
      {
         get
         {
            if ( _disposed )
            {
               throw new ObjectDisposedException("The SingletonController object has already been disposed.");
            }
            return _isFirstInstance;
         }
      }

      /// <summary>
      /// Gets the single instance enforcer (the first instance of the application) which would receive messages.
      /// </summary>
      public ISingletonEnforcer Enforcer
      {
         get
         {
            if ( _disposed )
            {
               throw new ObjectDisposedException("The SingletonController object has already been disposed.");
            }
            return _proxy.Enforcer;
         }
      }

      /// <summary>
      /// Instantiates a new SingletonController object to determine whether the application is already running.
      /// </summary>
      /// <param name="name">The unique name used to identify the application.</param>
      /// <param name="enforcer">The ISingletonEnforcer object that will handle messages.</param>
      /// <exception cref="System.ArgumentNullException">name is null or empty.</exception>
      /// <exception cref="SingletonException">A general error occured while trying to instantiate the SingletonController.</exception>
      public SingletonController(string name, ISingletonEnforcer enforcer)
      {
         if ( String.IsNullOrEmpty(name) )
         {
            throw new ArgumentNullException("name", "name cannot be null or empty.");
         }

         if ( enforcer == null )
         {
            throw new InvalidOperationException("the ISingletonInstanceEnforcer cannot be null");
         }

         try
         {
            _singleInstanceMutex = new Mutex(true, name, out _isFirstInstance);

            string proxyObjectName = "SingletonProxy";
            string proxyUri = "ipc://" + name + "/" + proxyObjectName;

            // If no previous instance was found, create a server channel which will provide the proxy to the first created instance
            if ( _isFirstInstance )
            {
               // Create an IPC server channel to listen for SingletonProxy object requests
               _ipcChannel = new IpcServerChannel(name);
               // Register the channel and get it ready for use
               ChannelServices.RegisterChannel(_ipcChannel, false);
               // Register the service which gets the SingletonProxy object, so it can be accessible by IPC client channels
               RemotingConfiguration.RegisterWellKnownServiceType(typeof(SingletonProxy), proxyObjectName, WellKnownObjectMode.Singleton);

               // Create the first proxy object
               _proxy = new SingletonProxy(enforcer);
               // Publish the first proxy object so IPC clients requesting a proxy would receive a reference to it
               RemotingServices.Marshal(_proxy, proxyObjectName);
            }
            else
            {
               // Create an IPC client channel to request the existing SingletonProxy object.
               _ipcChannel = new IpcClientChannel();
               // Register the channel and get it ready for use
               ChannelServices.RegisterChannel(_ipcChannel, false);

               // Retreive a reference to the proxy object which will be later used to send messages
               _proxy = (SingletonProxy)Activator.GetObject(typeof(SingletonProxy), proxyUri);
            }
         }
         catch ( Exception ex )
         {
            throw new SingletonException("Failed to instantiate a new SingletonController object. See InnerException for more details.", ex);
         }
      }

      /// <summary>
      /// Releases all unmanaged resources used by the object.
      /// </summary>
      ~SingletonController()
      {
         Dispose(false);
      }

      /// <summary>
      /// Releases all unmanaged resources used by the object, and potentially releases managed resources.
      /// </summary>
      /// <param name="disposing">true to dispose of managed resources; otherwise false.</param>
      protected virtual void Dispose(bool disposing)
      {
         if ( !_disposed )
         {
            if ( disposing )
            {
               if ( _singleInstanceMutex != null )
               {
                  _singleInstanceMutex.Close();
                  _singleInstanceMutex = null;
               }

               if ( _ipcChannel != null )
               {
                  ChannelServices.UnregisterChannel(_ipcChannel);
                  _ipcChannel = null;
               }
            }

            _disposed = true;
         }
      }

      /// <summary>
      /// Releases all resources used by the object.
      /// </summary>
      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Sends a message to the first instance of the application.
      /// </summary>
      /// <param name="args">The message to send to the first instance of the application. The message must be serializable.</param>
      /// <exception cref="System.InvalidOperationException">The object was constructed with the SingletonController(string name) constructor overload, or with the SingletonController(string name, SingleInstanceEnforcerRetriever enforcerRetriever) cosntructor overload, with enforcerRetriever set to null.</exception>
      /// <exception cref="SingletonException">The SingletonController has failed to send the message to the first application instance. The first instance might have terminated.</exception>
      public void SendMessageToFirstInstance(string[] args)
      {
         if (_disposed)
         {
            throw new ObjectDisposedException("The SingletonController object has already been disposed.");
         }

         if (_ipcChannel == null)
         {
            throw new InvalidOperationException("You cannot send messages to the first instance. The IPC channel is not initialized.");
         }

         try
         {
            _proxy.Enforcer.OnMessageReceived(args);
         }
         catch ( Exception ex )
         {
            throw new SingletonException("Failed to send message to the first instance of the application. The first instance might have terminated.", ex);
         }
      }

   }
}
