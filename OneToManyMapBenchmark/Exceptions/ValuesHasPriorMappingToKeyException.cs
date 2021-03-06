﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace OneToManyMapBenchmark
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class ValuesHasPriorMappingToKeyException : Exception
    {
        public ValuesHasPriorMappingToKeyException() { }
        public ValuesHasPriorMappingToKeyException(string message) : base(message) { }
        public ValuesHasPriorMappingToKeyException(string message, Exception inner) : base(message, inner) { }
        private ValuesHasPriorMappingToKeyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
