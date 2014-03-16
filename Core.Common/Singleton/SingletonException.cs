using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Common.Singleton
{
   /// <summary>
   /// Represents errors occured while trying to enforce single application instancing.
   /// </summary>
   [Serializable()]
   public class SingletonException : Exception
   {
      /// <summary>
      /// Instantiates a new SingletonException object.
      /// </summary>
      public SingletonException() { }

      /// <summary>
      /// Instantiates a new SingletonException object.
      /// </summary>
      /// <param name="message">The message that describes the error.</param>
      public SingletonException(string message)
         : base(message) { }

      /// <summary>
      /// Instantiates a new SingletonException object.
      /// </summary>
      /// <param name="message">The message that describes the error.</param>
      /// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
      public SingletonException(string message, Exception inner)
         : base(message, inner) { }

      /// <summary>
      /// Instantiates a new SingletonException object with serialized data.
      /// </summary>
      /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
      /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
      protected SingletonException(SerializationInfo info, StreamingContext context)
         : base(info, context) { }
   }
}
