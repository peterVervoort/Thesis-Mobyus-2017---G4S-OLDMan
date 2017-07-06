using AutoMapper;

namespace G4S.Business
{
    internal class StringToNullableBoolConverter : ITypeConverter<string, bool?>
    {
      
        public bool? Convert(ResolutionContext context)
        {
            string source = context.SourceValue.ToString();
            source = source.Trim();

            if (source.Equals("1", System.StringComparison.InvariantCultureIgnoreCase)) return true;
            else if (source.Equals("0", System.StringComparison.InvariantCultureIgnoreCase)) return false;
            return null;
        }
        
    }
}