using System;

namespace Core.Common.Singleton
{
   /// <summary>
   /// Provides methods which a single instance application can use to in order to be respond to any new instance of it.
   /// </summary>
   public interface ISingletonEnforcer
   {
      /// <summary>
      /// Handles messages received from a new instance of the application.
      /// </summary>
      /// <param name="args">The command line arguments received by a new instance of the application.</param>
      void OnMessageReceived(string[] args);
   }
}
