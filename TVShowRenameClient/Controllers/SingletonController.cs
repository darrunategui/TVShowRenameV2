using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace TVShowRename
{
   /// <summary>
   /// Handles communication between multiple instances of this application. Allows command line arguments to be passed from one instance to another.
   /// </summary>
   public class SingletonController : MarshalByRefObject
   {
      /// <summary>
      /// To listen for incoming messages from another instance of this application.
      /// </summary>
      private static TcpChannel tcpChannel = null;

      /// <summary>
      /// Randomly chosen port number to listen on.
      /// </summary>
      private static int port = 52457;

      /// <summary>
      /// Defines a method signature to receive an array of strings (command line arguments) and returns void.
      /// </summary>
      /// <param name="args"></param>
      public delegate void ReceiveDelegate(string[] args);

      /// <summary>
      /// Instance of the ReceveiveDelegate. Will be assigned the method to execute upon receiving command line arguments. 
      /// </summary>
      static public ReceiveDelegate Receiver { get; set; }

      /// <summary>
      /// Gets a previous instance of the specified process.
      /// </summary>
      /// <remarks>
      /// If a previous instance of the specified process is not found, then the <paramref name="process"/> parameter gets a default value of null.
      /// This method is called to ensure that there is only one running instance of the application.
      /// </remarks>
      /// <param name="currentProcess">The process of which to get a previous instance of.</param>
      /// <param name="process">When this method returns, contains the process of a previous instance of the application if one is found; otherwise, null.</param>
      /// <returns>true if the a previous instance is found; otherwise, false.</returns>
      public static bool TryGetPreviousInstance(Process currentProcess, out Process process)
      {
         // Loop through all the processes with the same name.
         foreach (Process existingProcess in Process.GetProcessesByName(currentProcess.ProcessName))
         {
            if ((existingProcess.Id != currentProcess.Id) && (existingProcess.MainModule.FileName == currentProcess.MainModule.FileName))
            {
               process = existingProcess;
               return true;
            }
         }
         process = null;
         return false;
      }

      /// <summary>
      /// Registers a call back function to execute when command line arguments are received from another instance of the application.
      /// </summary>
      /// <param name="del">The delegate to execute upon receiving command line arguments.</param>
      public static void RegisterReceiveEvent(ReceiveDelegate del)
      {
         // Initialize a server channel to listen on the specified port
         tcpChannel = new TcpChannel(port);

         // Register as a well known service so other intances of this application can get a proxy to interact with this singleton controller
         ChannelServices.RegisterChannel(tcpChannel, false);
         RemotingConfiguration.RegisterWellKnownServiceType(typeof(SingletonController), typeof(SingletonController).Name, WellKnownObjectMode.SingleCall);
         Receiver += del;
      }

      /// <summary>
      /// Sends the given arguments to the single running instance of the application.
      /// </summary>
      /// <param name="args">Command line arguments.</param>
      public static void SendArguments(string[] args)
      {
         SingletonController controller;

         // Initializes a client channel to send the message.
         TcpChannel channel = new TcpChannel();
         ChannelServices.RegisterChannel(channel, false);
         try
         {
            // Retrieves a proxy to send the arguments to the currently running instance of Developer Workbench.
            controller = (SingletonController)Activator.GetObject(typeof(SingletonController), 
                                                                  String.Format("tcp://localhost:{0}/{1}", port, typeof(SingletonController).Name));
         }
         catch (Exception e)
         {
            Console.WriteLine("Exception: " + e.Message);
            throw;
         }

         // Send the command line arguments
         controller.Receive(args);
      }

      /// <summary>
      /// Executes the call back function setup to receive the command line arguments.
      /// </summary>
      /// <param name="s">Command line arguments.</param>
      public void Receive(string[] s)
      {
         if (Receiver != null)
         {
            Receiver(s);
         }
      }

      /// <summary>
      /// Frees any used resources.
      /// </summary>
      public static void Cleanup()
      {
         if (tcpChannel != null)
         {
            tcpChannel.StopListening(null);
         }
         tcpChannel = null;
      }

   }
}
