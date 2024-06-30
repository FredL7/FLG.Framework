using System.Text;

namespace FLG.Cs.Networking {
    internal enum Messages {
        WELCOME = 0, HEARTBEAT = 1, COMMAND = 2
    }

    internal class Message : IDisposable {
        internal const int DATA_BUFFER_SIZE = 4096;

        //private const int BYTE_LENGTH = 1;
        public const int BOOL_LENGTH = 1;
        // private const int SHORT_LENGTH = 2;
        public const int INT_LENGTH = 4;
        // private const int LONG_LENGTH = 8;
        public const int FLOAT_LENGTH = 4;

        private List<byte> _buffer = new();
        private byte[] _readableBuffer = Array.Empty<byte>();
        private int _readPos = 0;

        public int Length { get => _buffer.Count; }
        public int UnreadLength { get => Length - _readPos; }

        private StringBuilder _debugMessage = new();

        public Message() { }

        public Message(int id)
        {
            Write(id, "msgType");
        }

        public Message(byte[] data)
        {
            SetBytes(data);
        }

        public void SetBytes(byte[] data)
        {
            Write(data);
            _readableBuffer = _buffer.ToArray();
        }

        public void WriteLength()
        {
            _buffer.InsertRange(0, BitConverter.GetBytes(_buffer.Count));
            _debugMessage.Insert(0, "//length:" + _buffer.Count);
        }

        public void WriteId(int id)
        {
            _buffer.InsertRange(0, BitConverter.GetBytes(id));
            _debugMessage.Insert(0, "//id:" + id);
        }

        public byte[] ToArray()
        {
            _readableBuffer = _buffer.ToArray();
            return _readableBuffer;
        }

        public override string ToString()
        {
            return _debugMessage.ToString();
        }

        public void Reset(bool shouldReset = true)
        {
            if (shouldReset)
            {
                _buffer.Clear();
                _readableBuffer = Array.Empty<byte>();
                _readPos = 0;
                _debugMessage.Clear();
            }
            else
            {
                _readPos -= INT_LENGTH;
            }
        }

        #region Write
        // public void Write(byte value { _buffer.Add(value); }
        public void Write(byte[] value)
        {
            _buffer.AddRange(value);
        }
        // public void Write(short value) { _buffer.AddRange(BitConverter.GetBytes(value)); }
        public void Write(int value, string debugId = "int")
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
            _debugMessage.Append($"//{debugId}:" + value);
        }
        // public void Write(long value) { _buffer.AddRange(BitConverter.GetBytes(value)); }
        public void Write(float value, string debugId = "float")
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
            _debugMessage.Append($"//{debugId}:" + value);
        }
        public void Write(bool value, string debugId = "bool")
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
            _debugMessage.Append($"//{debugId}:" + value);
        }
        public void Write(string value, string debugId = "string")
        {
            Write(value.Length, "strlen");
            _buffer.AddRange(Encoding.ASCII.GetBytes(value));
            _debugMessage.Append($"//{debugId}:" + value);
        }
        #endregion Write

        #region Read
        /*public byte ReadByte(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                byte _value = _readableBuffer[_readPos];
                if (moveReadPos)
                {
                    _readPos += BYTE_LENGTH;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type \"byte\"");
            }
        }*/

        public byte[] ReadBytes(int length, bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                byte[] _value = _buffer.GetRange(_readPos, length).ToArray();
                if (moveReadPos)
                {
                    _readPos += length;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type \"byte[]\"");
            }
        }

        /*public short ReadShort(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                short _value = BitConverter.ToInt16(_readableBuffer, _readPos);
                if (moveReadPos)
                {
                    _readPos += SHORT_LENGTH;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type \"short\"");
            }
        }*/

        public int ReadInt(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                int _value = BitConverter.ToInt32(_readableBuffer, _readPos);
                if (moveReadPos)
                {
                    _readPos += INT_LENGTH;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type \"int\"");
            }
        }

        /*public long ReadLong(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                long _value = BitConverter.ToInt64(_readableBuffer, _readPos);
                if (moveReadPos)
                {
                    _readPos += LONG_LENGTH;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type \"long\"");
            }
        }*/

        public float ReadFloat(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                float _value = BitConverter.ToSingle(_readableBuffer, _readPos);
                if (moveReadPos)
                {
                    _readPos += FLOAT_LENGTH;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type \"float\"");
            }
        }

        public bool ReadBool(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                bool _value = BitConverter.ToBoolean(_readableBuffer, _readPos);
                if (moveReadPos)
                {
                    _readPos += BOOL_LENGTH;
                }
                return _value;
            }
            else
            {
                throw new Exception("Could not read value of type \"bool\"");
            }
        }

        public string ReadString(bool moveReadPos = true)
        {
            try
            {
                int _length = ReadInt();
                string _value = Encoding.ASCII.GetString(_readableBuffer, _readPos, _length);
                if (moveReadPos && _value.Length > 0)
                {
                    _readPos += _length;
                }
                return _value;
            }
            catch
            {
                throw new Exception("Could not read value of type \"string\"");
            }
        }
        #endregion Read

        #region IDisposable
        private bool _disposed = false;
        protected virtual void Dispose(bool _disposing)
        {
            if (!_disposed)
            {
                if (_disposing)
                {
                    _buffer = new();
                    _readableBuffer = Array.Empty<byte>();
                    _readPos = 0;
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable
    }
}
