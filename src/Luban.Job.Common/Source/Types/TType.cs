using Luban.Job.Common.Defs;
using Luban.Job.Common.TypeVisitors;
using System.Collections.Generic;

namespace Luban.Job.Common.Types
{
    public abstract class TType
    {
        public bool IsNullable { get; }

        public Dictionary<string, string> Tags { get; }

        public List<IProcessor> Processors { get; } = new List<IProcessor>();

        protected TType(bool isNullable, Dictionary<string, string> tags)
        {
            IsNullable = isNullable;
            Tags = tags ?? new Dictionary<string, string>();
        }

        public bool HasTag(string attrName)
        {
            return Tags != null && Tags.ContainsKey(attrName);
        }

        public string GetTag(string attrName)
        {
            return Tags != null && Tags.TryGetValue(attrName, out var value) ? value : null;
        }

        public abstract bool TryParseFrom(string s);

        public virtual void Compile(DefFieldBase field)
        {
            foreach (var p in Processors)
            {
                p.Compile(field);
            }
        }

        public virtual bool IsCollection => false;

        public virtual bool IsBean => false;

        public virtual TType ElementType => null;

        public abstract void Apply<T>(ITypeActionVisitor<T> visitor, T x);
        public abstract void Apply<T1, T2>(ITypeActionVisitor<T1, T2> visitor, T1 x, T2 y);
        public abstract TR Apply<TR>(ITypeFuncVisitor<TR> visitor);
        public abstract TR Apply<T, TR>(ITypeFuncVisitor<T, TR> visitor, T x);
        public abstract TR Apply<T1, T2, TR>(ITypeFuncVisitor<T1, T2, TR> visitor, T1 x, T2 y);
        public abstract TR Apply<T1, T2, T3, TR>(ITypeFuncVisitor<T1, T2, T3, TR> visitor, T1 x, T2 y, T3 z);
    }
}
