﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAspNet.Domain
{
    // from "Value Object: a better implementation" by Vladimir Khorikov (https://enterprisecraftsmanship.com/posts/value-object-better-implementation/)
    // TODO_JBOY: go to the CSharpFunctionalExtensions GitHub repository later to get the newest version of this class
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;
            var valueObject = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}
