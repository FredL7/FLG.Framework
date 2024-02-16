namespace FLG.Cs.UI.Grids {
    public enum EGridDirection { NORMAL, REVERSE }

    internal class EGridDirectionExtension {
        internal static EGridDirection FromString(string value)
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