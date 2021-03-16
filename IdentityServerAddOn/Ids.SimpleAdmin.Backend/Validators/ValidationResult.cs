using System.Collections.Generic;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ValidationResult
    {
        private readonly Dictionary<string, List<string>> _result;
        public readonly bool IsValid;
        public ValidationResult(Dictionary<string, List<string>> result)
        {
            _result = result;
            IsValid = result.Count == 0;
        }
        public ValidationResult()
        {
            _result = new Dictionary<string, List<string>>();
        }

        public List<string> this[string key]
        {
            get
            {
                if (!_result.ContainsKey(key)) return null;
                return _result[key];
            }
        }
    }
}
