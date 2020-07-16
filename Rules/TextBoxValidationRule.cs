using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DauBe_WTF.Rules
{
    public class TextBoxValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var validationResult = new ValidationResult(true, null);

            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    var regex = new Regex(@"-?\d+"); //regex that matches disallowed text
                    var parsingNotOk = regex.IsMatch(value.ToString());
                    if (!parsingNotOk)
                    {
                        validationResult = new ValidationResult(false, "Illegal Characters, Please Enter Numeric Value");
                    }
                }
            }

            return validationResult;
        }
    }
}
