using System;
using System.Globalization;

namespace SHOJIsCode.Libraries {
    /// <summary>
    /// �����񏑎������\��Boolean�^
    /// </summary>
    public struct FormattableBoolean : IFormattable {
        bool b;

        #region �R���X�g���N�^
        /// <summary>
        /// �����񏑎������\��Boolean�^���쐬���A�l��ێ����܂�
        /// </summary>
        /// <param name="value"></param>
        public FormattableBoolean(object value) {
            b = Convert.ToBoolean(value);
        }
        #endregion

        /// <summary>
        /// ���̃C���X�^���X���ێ����Ă���Boolean�^�Ɠ��������ǂ������擾���܂�
        /// </summary>
        /// <param name="obj">���̃C���X�^���X�Ɣ�r����l(FormattableBoolean�^��������Boolean�^)</param>
        /// <returns>�������Ƃ� true</returns>
        public override bool Equals(object obj) {
            if ( obj is FormattableBoolean ) return b.Equals((bool)obj);
            return b.Equals(obj);
        }

        /// <summary>
        /// ���̃C���X�^���X�̃n�b�V���R�[�h��Ԃ��܂�
        /// </summary>
        /// <returns>�n�b�V���R�[�h</returns>
        public override int GetHashCode() {
            return b.GetHashCode();
        }

        /// <summary>
        /// ���̃C���X�^���X��\���������Ԃ��܂�
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return b.ToString();
        }

        #region �L���X�g
        /// <summary>
        /// FormattableBoolean�^����Boolean�^�ւ̃L���X�g
        /// </summary>
        /// <param name="value">FormttableBoolean�^</param>
        /// <returns></returns>
        public static implicit operator bool(FormattableBoolean value) { return value.b; }
        public static implicit operator FormattableBoolean(bool value) { return new FormattableBoolean(value); }
        #endregion

        #region ���Z�q�̃I�[�o�[���[�h
        public static bool operator true(FormattableBoolean value) { return value.b; }
        public static bool operator false(FormattableBoolean value) { return !value.b; }

        public static FormattableBoolean operator !(FormattableBoolean value) { return new FormattableBoolean(!value.b); }

        public static FormattableBoolean operator &(FormattableBoolean value1, bool value2) { return new FormattableBoolean(value1.b && value2); }
        public static FormattableBoolean operator |(FormattableBoolean value1, bool value2) { return new FormattableBoolean(value1.b || value2); }
        #endregion

        #region IFormattable�C���^�[�t�F�C�X
        /// <summary>
        /// �������w�肵�Ă��̃C���X�^���X�𕶎��񉻂��܂�
        /// </summary>
        /// <param name="format">����������</param>
        /// <param name="formatProvider">�t�H�[�}�b�g�v���o�C�_</param>
        /// <returns>���������ꂽ������</returns>
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
using System;
using System.Globalization;

namespace SHOJIsCode.Libraries {
    /// <summary>
    /// �����񏑎������\��Boolean�^
    /// </summary>
    public struct FormattableBoolean : IFormattable {
        bool b;

        #region �R���X�g���N�^
        /// <summary>
        /// �����񏑎������\��Boolean�^���쐬���A�l��ێ����܂�
        /// </summary>
        /// <param name="value"></param>
        public FormattableBoolean(object value) {
            b = Convert.ToBoolean(value);
        }
        #endregion

        /// <summary>
        /// ���̃C���X�^���X���ێ����Ă���Boolean�^�Ɠ��������ǂ������擾���܂�
        /// </summary>
        /// <param name="obj">���̃C���X�^���X�Ɣ�r����l(FormattableBoolean�^��������Boolean�^)</param>
        /// <returns>�������Ƃ� true</returns>
        public override bool Equals(object obj) {
            if ( obj is FormattableBoolean ) return b.Equals((bool)obj);
            return b.Equals(obj);
        }

        /// <summary>
        /// ���̃C���X�^���X�̃n�b�V���R�[�h��Ԃ��܂�
        /// </summary>
        /// <returns>�n�b�V���R�[�h</returns>
        public override int GetHashCode() {
            return b.GetHashCode();
        }

        /// <summary>
        /// ���̃C���X�^���X��\���������Ԃ��܂�
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return b.ToString();
        }

        #region �L���X�g
        public static implicit operator bool(FormattableBoolean value) { return value.b; }
        public static implicit operator FormattableBoolean(bool value) { return new FormattableBoolean(value); }
        #endregion

        #region ���Z�q�̃I�[�o�[���[�h
        public static bool operator true(FormattableBoolean value) { return value.b; }
        public static bool operator false(FormattableBoolean value) { return !value.b; }

        public static FormattableBoolean operator !(FormattableBoolean value) { return new FormattableBoolean(!value.b); }

        public static FormattableBoolean operator &(FormattableBoolean value1, bool value2) { return new FormattableBoolean(value1.b && value2); }
        public static FormattableBoolean operator |(FormattableBoolean value1, bool value2) { return new FormattableBoolean(value1.b || value2); }
        #endregion

        #region IFormattable�C���^�[�t�F�C�X
        /// <summary>
        /// �������w�肵�Ă��̃C���X�^���X�𕶎��񉻂��܂�
        /// </summary>
        /// <param name="format">����������</param>
        /// <param name="formatProvider">�t�H�[�}�b�g�v���o�C�_</param>
        /// <returns>���������ꂽ������</returns>
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
            }
        }
        #endregion
    }
}

