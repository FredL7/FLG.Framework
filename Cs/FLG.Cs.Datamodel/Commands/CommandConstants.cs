namespace FLG.Cs.Datamodel.Commands {
    public static class CommandConstants {
        public const string REFLECTION_CLASSMETHOD_SEPARATOR = "&&&";
        public const string REFLECTION_PARAM_SEPARATOR = "///";
        public const string REFLECTION_TYPE_SEPARATOR = ":::";

        public const string REFLECTION_TYPE_BOOL = "b";
        public const string REFLECTION_TYPEANDSEPARATOR_BOOL = REFLECTION_TYPE_BOOL + REFLECTION_TYPE_SEPARATOR;
        public const string REFLECTION_TYPE_INT = "i";
        public const string REFLECTION_TYPEANDSEPARATOR_INT = REFLECTION_TYPE_INT + REFLECTION_TYPE_SEPARATOR;
        public const string REFLECTION_TYPE_FLOAT = "f";
        public const string REFLECTION_TYPEANDSEPARATOR_FLOAT = REFLECTION_TYPE_FLOAT + REFLECTION_TYPE_SEPARATOR;
        public const string REFLECTION_TYPE_STRING = "s";
        public const string REFLECTION_TYPEANDSEPARATOR_STRING = REFLECTION_TYPE_STRING + REFLECTION_TYPE_SEPARATOR;
    }
}
