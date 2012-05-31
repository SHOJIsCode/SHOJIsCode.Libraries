using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHOJIsCode.Libraries {
    /// <summary>
    /// Boolean用のカスタムフォーマッター
    /// </summary>
    public class BooleanFormatter : IFormatProvider, ICustomFormatter {
        public object GetFormat(Type formatType) {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider) {
            if ( arg is bool ) arg = new FormattableBoolean(arg);
            if ( arg is IFormattable ) return ((IFormattable)arg).ToString(format, formatProvider);
            if ( arg != null ) return arg.ToString();
            return string.Empty;
        }
    }
}
