using System;

namespace Core.Common.Singleton
{
   /// <summary>
   /// Provides a proxy to communicate with the first instance of the application.
   /// </summary>
   public class SingletonProxy : MarshalByRefObject
   {
      private ISingletonEnforcer _enforcer;

      /// <summary>
      /// Gets the enforcer (first instance of the application) which will receive messages from the new instances of the application.
      /// </summary>
      public ISingletonEnforcer Enforcer
      {
         get { return _enforcer; }
      }
      
      /// <summary>
      /// Instantiates a new SingletonProxy object.
      /// </summary>
      /// <param name="enforcer">The enforcer (first instance of the application) which will receive messages from the new instances of the application.</param>
      /// <exception cref="ArgumentNullException">Thrown when the enforcer is null.</exception>
      public SingletonProxy(ISingletonEnforcer enforcer)
      {
         if ( enforcer == null )
         {
            throw new ArgumentNullException("enforcer", "enforcer cannot be null.");
         }

         _enforcer = enforcer;
      }

      #region Overriden MarshalByRefObject Members
      /// <summary>
      /// Obtains a lifetime service object to control the lifetime policy for this instance.
      /// </summary>
      /// <remarks>The purpose of returning null here is to ensure the object lives forever. This way it won't be collected by the garbage collector.</remarks>
      /// <returns>
      /// An object of type System.Runtime.Remoting.Lifetime.ILease used to control the lifetime policy for this instance. 
      /// This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object 
      /// initialized to the value of the System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime property.
      /// </returns>
      /// <exception cref="System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
      public override object InitializeLifetimeService()
      {
         return null;
      }
      #endregion
   }
}
