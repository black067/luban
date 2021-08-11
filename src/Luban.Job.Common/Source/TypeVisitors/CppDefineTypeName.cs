using Luban.Job.Common.Types;

namespace Luban.Job.Common.TypeVisitors
{
    public class CppDefineTypeName : DecoratorFuncVisitor<string>
    {
        public static CppDefineTypeName Ins { get; } = new CppDefineTypeName();

        public override string DoAccept(TType type)
        {
            //return type.IsNullable ? $"std::optional<{type.Apply(CppUnderingDefineTypeName.Ins)}>" : type.Apply(CppUnderingDefineTypeName.Ins);
            return type.IsNullable ? $"std::shared_ptr<{type.Apply(CppSharedPtrUnderingDefineTypeName.Ins)}>" : type.Apply(CppSharedPtrUnderingDefineTypeName.Ins);
        }

        public override string Accept(TBean type)
        {
            return type.Apply(CppSharedPtrUnderingDefineTypeName.Ins);
        }
    }
}
