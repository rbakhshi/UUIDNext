﻿using System;
using System.Linq;
using System.Reflection;
using NFluent;
using UUIDNext.Generator;

namespace UUIDNext.Test
{
    internal static class UuidTestHelper
    {
        public const string LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt";
        public static void CheckVersionAndVariant(Guid uuid, byte version)
        {
            var strUuid = uuid.ToString();
            Check.That(strUuid[14]).IsEqualTo(version.ToString().Single());
            Check.That(strUuid[19]).IsOneOf('8', '9', 'a', 'b', 'A', 'B');
        }

        public static bool TryGenerateNew(this UuidTimestampGeneratorBase generator, DateTime date, out Guid newUuid)
        {
            var tryGenerateNewMethod = generator.GetType().GetMethod("TryGenerateNew", BindingFlags.Instance | BindingFlags.NonPublic);
            object[] parameters = { date, Guid.Empty };
            object result = tryGenerateNewMethod.Invoke(generator, parameters);
            newUuid = (Guid)parameters[1];
            return (bool)result;
        }

        public static int GetSequenceMaxValue(this UuidTimestampGeneratorBase generator)
        {
            var sequenceMaxValueProperty = generator.GetType().GetProperty("SequenceMaxValue", BindingFlags.Instance | BindingFlags.NonPublic);
            return (int)sequenceMaxValueProperty.GetValue(generator, null);
        }

    }
}
