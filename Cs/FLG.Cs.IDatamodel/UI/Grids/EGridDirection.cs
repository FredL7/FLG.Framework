namespace FLG.Cs.Datamodel {
    public enum EGridDirection { NORMAL, REVERSE }

    public class EGridDirectionExtension {
        public static EGridDirection FromString(string value)
        {
            return value.ToLower() switch
            {
                "normal" => EGridDirection.NORMAL,
                "reverse" => EGridDirection.REVERSE,
                _ => EGridDirection.NORMAL,
            };
        }
    }
}