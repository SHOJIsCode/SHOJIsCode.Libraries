using System;
using System.Globalization;

namespace SHOJIsCode.Libraries {
    /// <summary>
    /// 文字列書式化が可能なBoolean型
    /// </summary>
    public struct FormattableBoolean : IFormattable {
        bool b;

        #region コンストラクタ
        /// <summary>
        /// 文字列書式化が可能なBoolean型を作成し、値を保持します
        /// </summary>
        /// <param name="value"></param>
        public FormattableBoolean(object value) {
            b = Convert.ToBoolean(value);
        }
        #endregion

        /// <summary>
        /// このインスタンスが保持しているBoolean型と等しいかどうかを取得します
        /// </summary>
        /// <param name="obj">このインスタンスと比較する値(FormattableBoolean型もしくはBoolean型)</param>
        /// <returns>等しいとき true</returns>
        public override bool Equals(object obj) {
            if ( obj is FormattableBoolean ) return b.Equals((bool)obj);
            return b.Equals(obj);
        }

        /// <summary>
        /// このインスタンスのハッシュコードを返します
        /// </summary>
        /// <returns>ハッシュコード</returns>
        public override int GetHashCode() {
            return b.GetHashCode();
        }

        /// <summary>
        /// このインスタンスを表す文字列を返します
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return b.ToString();
        }

        #region キャスト
        public static implicit operator bool(FormattableBoolean value) { return value.b; }
        public static implicit operator FormattableBoolean(bool value) { return new FormattableBoolean(value); }
        #endregion

        #region 演算子のオーバーロード
        public static bool operator true(FormattableBoolean value) { return value.b; }
        public static bool operator false(FormattableBoolean value) { return !value.b; }

        public static FormattableBoolean operator !(FormattableBoolean value) { return new FormattableBoolean(!value.b); }

        public static FormattableBoolean operator &(FormattableBoolean value1, bool value2) { return new FormattableBoolean(value1.b && value2); }
        public static FormattableBoolean operator |(FormattableBoolean value1, bool value2) { return new FormattableBoolean(value1.b || value2); }
        #endregion

        #region IFormattableインターフェイス
        /// <summary>
        /// 書式を指定してこのインスタンスを文字列化します
        /// </summary>
        /// <param name="format">書式文字列</param>
        /// <param name="formatProvider">フォーマットプロバイダ</param>
        /// <returns>書式化された文字列</returns>
        public string ToString(string format, IFormatProvider formatProvider) {
            if ( format == null || format == string.Empty ) return b.ToString();

            switch ( format ) {
                case "T": return b ? "TRUE" : "FALSE";
                case "t": return b ? "true" : "false";
                case "Y": return b ? "YES" : "NO";
                case "y": return b ? "yes" : "no";
                case "O": return b ? "ON" : "OFF";
                case "o": return b ? "on" : "off";
                case "0": return b ? "1" : "0";

                default:
                    string[] p = format.Split(';');
                    string st = p.Length > 0 ? p[0] : string.Empty;
                    string sf = p.Length > 1 ? p[1] : string.Empty;
                    return b ? st : sf;
            }
        }
        #endregion
    }
}
