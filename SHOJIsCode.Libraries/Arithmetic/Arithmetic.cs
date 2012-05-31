using System;
using System.Linq.Expressions;

namespace SHOJIsCode.Libraries {
    /// <summary>
    /// 型パラメータ同士の演算ができるようにする構造体
    /// </summary>
    /// <typeparam name="T">関連付ける型</typeparam>
    public struct Arithmetic<T>
        : IComparable, IArithmetic
        where T : struct, IComparable {

        #region 演算用デリゲート
        /// <summary>
        /// 単項演算用デリゲート
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private delegate T UnaryOperator(T value);

        /// <summary>
        /// 二項演算用デリゲート
        /// </summary>
        /// <param name="value1">値1</param>
        /// <param name="value2">値2</param>
        /// <returns>結果</returns>
        private delegate T BinaryOperator(T value1, T value2);

        /// <summary>
        /// 変換用デリゲート
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>結果</returns>
        private delegate T Converter(object value);

        /// <summary>
        /// シフト用デリゲート
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="bitcount"></param>
        /// <returns></returns>
        private delegate T Shifter(T value, int bits);
        #endregion

        #region メンバ変数
        /// <summary>
        /// 値
        /// </summary>
        private T _value;

        /// <summary>
        /// 演算パラメータ x
        /// </summary>
        private static readonly ParameterExpression x = Expression.Parameter(typeof(T), "x");

        /// <summary>
        /// 演算パラメータ y
        /// </summary>
        private static readonly ParameterExpression y = Expression.Parameter(typeof(T), "y");

        /// <summary>
        /// 変換用パラメータ o
        /// </summary>
        private static readonly ParameterExpression o = Expression.Parameter(typeof(object), "o");

        /// <summary>
        /// シフト用パラメータn
        /// </summary>
        private static readonly ParameterExpression n = Expression.Parameter(typeof(int), "n");

        /// <summary>
        /// １を示す値
        /// </summary>
        private static T One = default(T);

        /// <summary>
        /// 値変換デリゲート
        /// </summary>
        private static Converter Convert = null;

        private static BinaryOperator Add = null;

        private static BinaryOperator Subtract = null;

        private static BinaryOperator Multiply = null;

        private static BinaryOperator Divide = null;

        private static UnaryOperator Negate = null;

        private static UnaryOperator Plus = null;

        private static BinaryOperator Module = null;

        private static BinaryOperator And = null;

        private static BinaryOperator Or = null;

        private static BinaryOperator ExclusiveOr = null;

        private static UnaryOperator Not = null;

        private static Shifter ShiftLeft = null;

        private static Shifter ShiftRight = null;
        #endregion

        /// <summary>
        /// ゼロもしくはNULLを示すArithmetic構造体
        /// </summary>
        public static readonly Arithmetic<T> ZeroOrNull = new Arithmetic<T>(default(T));

        #region コンストラクタ
        /// <summary>
        /// Arithmetic構造体を作成し、値を設定します
        /// </summary>
        /// <param name="value">値</param>
        public Arithmetic(T value) {
            _value = value;
        }
        #endregion

        /// <summary>
        /// 値を設定または取得します
        /// </summary>
        public T Value {
            get {
                return _value;
            }
            set {
                _value = value;
            }
        }

        /// <summary>
        /// 値を示すオブジェクトを取得します
        /// </summary>
        public object Object { get { return Value; } }

        /// <summary>
        /// 値の型を取得します
        /// </summary>
        public Type ValueType { get { return typeof(T); } }

        /// <summary>
        /// 値をArithmetic構造体にキャストします
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>Arithmetic構造体</returns>
        public static Arithmetic<T> Cast(object value) {
            if ( Convert == null ) Convert = CreateDelegate<Converter>(Expression.Convert(o, typeof(T)), o);
            return new Arithmetic<T>(Convert.Invoke(value));
        }

        /// <summary>
        /// Arithmetic構造体に設定された値を表す文字列を取得します
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString() {
            return Value.ToString();
        }

        /// <summary>
        /// Arithmetic構造体と値が一致するかどうかを取得します
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            if ( Value is IArithmetic ) return Value.Equals(obj);
            if ( obj is IArithmetic ) return obj.Equals(Value);
            return Value.Equals(obj);
        }

        /// <summary>
        /// Arithmetic構造体を比較します
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>結果</returns>
        public int CompareTo(object obj) {
            if (Value is IArithmetic) return Value.CompareTo(obj);
            if (obj is IArithmetic) return CompareTo(((IArithmetic)obj).Object);
            return Value.CompareTo(obj);
        }

        /// <summary>
        /// ハッシュコードを返す
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return this.Value.GetHashCode();
        }

        #region 演算オーバーロード
        /// <summary>
        /// 値をArithmetic構造体にキャストします
        /// </summary>
        /// <param name="arg">値</param>
        /// <returns>Arithmetic構造体</returns>
        public static implicit operator Arithmetic<T>(T arg) {
            return new Arithmetic<T>(arg);
        }

        /// <summary>
        /// Arithmetic構造体に設定された値を取得します
        /// </summary>
        /// <param name="arg">Arithmetic構造体</param>
        /// <returns>値</returns>
        public static implicit operator T(Arithmetic<T> arg) {
            return arg.Value;
        }

        public static Arithmetic<T> operator +(Arithmetic<T> arg1, T arg2) {
            if ( Add == null ) Add = CreateDelegate<BinaryOperator>(Expression.Add(x, y), x, y);
            return new Arithmetic<T>(Add(arg1.Value, arg2));
        }

        public static Arithmetic<T> operator -(Arithmetic<T> arg1, T arg2) {
            if ( Subtract == null ) Subtract = CreateDelegate<BinaryOperator>(Expression.Subtract(x, y), x, y);
            return new Arithmetic<T>(Subtract(arg1, arg2));
        }

        public static Arithmetic<T> operator *(Arithmetic<T> arg1, T arg2) {
            if ( Multiply == null ) Multiply = CreateDelegate<BinaryOperator>(Expression.Multiply(x, y), x, y);
            return new Arithmetic<T>(Multiply(arg1.Value, arg2));
        }

        public static Arithmetic<T> operator /(Arithmetic<T> arg1, T arg2) {
            if ( Divide == null ) Divide = CreateDelegate<BinaryOperator>(Expression.Divide(x, y), x, y);
            return new Arithmetic<T>(Divide(arg1.Value, arg2));
        }

        public static Arithmetic<T> operator %(Arithmetic<T> arg1, T arg2) {
            if ( Module == null ) Module = CreateDelegate<BinaryOperator>(Expression.Modulo(x, y), x, y);
            return new Arithmetic<T>(Module(arg1.Value, arg2));
        }

        public static Arithmetic<T> operator -(Arithmetic<T> arg) {
            if ( Negate == null ) Negate = CreateDelegate<UnaryOperator>(Expression.Negate(x), x);
            return new Arithmetic<T>(Negate(arg.Value));
        }

        public static Arithmetic<T> operator +(Arithmetic<T> arg) {
            return new Arithmetic<T>(arg.Value);
        }

        public static Arithmetic<T> operator ++(Arithmetic<T> arg) {
            if ( Add == null ) Add = CreateDelegate<BinaryOperator>(Expression.Add(x, y), x, y);
            if ( One.Equals(default(T)) ) One = Cast(1).Value;
            return new Arithmetic<T>(Add(arg.Value, One));
        }

        public static Arithmetic<T> operator --(Arithmetic<T> arg) {
            if ( Subtract == null ) Subtract = CreateDelegate<BinaryOperator>(Expression.Subtract(x, y), x, y);
            if ( One.Equals(default(T)) ) One = Cast(1).Value;
            return new Arithmetic<T>(Subtract(arg.Value, One));
        }

        public static Arithmetic<T> operator &(Arithmetic<T> arg1, Arithmetic<T> arg2) {
            if ( And == null ) And = CreateDelegate<BinaryOperator>(Expression.And(x, y), x, y);
            return new Arithmetic<T>(And(arg1.Value, arg2.Value));
        }

        public static Arithmetic<T> operator |(Arithmetic<T> arg1, Arithmetic<T> arg2) {
            if ( Or == null ) Or = CreateDelegate<BinaryOperator>(Expression.Or(x, y), x, y);
            return new Arithmetic<T>(Or(arg1.Value, arg2.Value));
        }

        public static Arithmetic<T> operator ^(Arithmetic<T> arg1, Arithmetic<T> arg2) {
            if ( ExclusiveOr == null ) ExclusiveOr = CreateDelegate<BinaryOperator>(Expression.ExclusiveOr(x, y), x, y);
            return new Arithmetic<T>(ExclusiveOr(arg1.Value, arg2.Value));
        }

        public static Arithmetic<T> operator ~(Arithmetic<T> arg) {
            if ( Not == null ) Not = CreateDelegate<UnaryOperator>(Expression.Not(x), x);
            return new Arithmetic<T>(Not(arg.Value));
        }

        public static Arithmetic<T> operator <<(Arithmetic<T> arg1, int arg2) {
            if (ShiftLeft == null ) ShiftLeft = CreateDelegate<Shifter>(Expression.LeftShift(x, n), x, n);
            return new Arithmetic<T>(ShiftLeft(arg1.Value, arg2));
        }

        public static Arithmetic<T> operator >>(Arithmetic<T> arg1, int arg2) {
            if ( ShiftRight == null ) ShiftRight = CreateDelegate<Shifter>(Expression.RightShift(x, n), x, n);
            return new Arithmetic<T>(ShiftRight(arg1.Value, arg2));
        }

        public static bool operator ==(Arithmetic<T> arg1, Arithmetic<T> arg2) {
            return arg1.Equals(arg2.Value);
        }

        public static bool operator !=(Arithmetic<T> arg1, Arithmetic<T> arg2) {
            return !arg1.Equals(arg2.Value);
        }

        public static bool operator >(Arithmetic<T> arg1, Arithmetic<T> arg2) {
            return arg1.CompareTo(arg2.Value) < 0;
        }

        public static bool operator >=(Arithmetic<T> arg1, Arithmetic<T> arg2) {
            return arg1.CompareTo(arg2.Value) <= 0;
        }

        public static bool operator <(Arithmetic<T> arg1, Arithmetic<T> arg2) {
            return arg1.CompareTo(arg2.Value) < 0;
        }

        public static bool operator <=(Arithmetic<T> arg1, Arithmetic<T> arg2) {
            return arg1.CompareTo(arg2.Value) <= 0;
        }
        #endregion

        #region プライベート関数
        /// <summary>
        /// 演算用のデリゲートを作成します
        /// </summary>
        /// <typeparam name="T">デリゲートの型</typeparam>
        /// <param name="body">演算を実行する式木</param>
        /// <param name="args">引数の式木</param>
        /// <returns>デリゲート</returns>
        private static T CreateDelegate<T>(Expression body, params ParameterExpression[] args) {
            return (T)(Expression.Lambda<T>(body, args).Compile());
        }
        #endregion
    }
    internal interface IArithmetic {
        object Object { get; }
    }
}
