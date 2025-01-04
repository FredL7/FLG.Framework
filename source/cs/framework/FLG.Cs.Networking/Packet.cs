using System.Text;


namespace FLG.Cs.Networking {
    internal class Packet : IDisposable {
        private List<byte> _buffer;
        private byte[] _readableBuffer;
        private int _readPos;

        public int Length { get => _buffer.Count; }
        public int UnreadLength { get => Length - _readPos; }

        // Generic ctor
        public Packet()
        {
            _buffer = [];
            _readableBuffer = [];
            _readPos = 0;
        }

        // Ctor for sending
        public Packet(int _id)
        {
            _buffer = [];
            _readableBuffer = [];
            _readPos = 0;

            Write(_id);
        }

        // Ctor for receiving
        public Packet(byte[] _data)
        {
            _buffer = [];
            _readableBuffer = [];
            _readPos = 0;

            SetBytes(_data);
        }

        #region Functions
        public void SetBytes(byte[] _data)
        {
            Write(_data);
            _readableBuffer = [.. _buffer];
        }

        public void WriteLength()
        {
            // Insert the byte length of the packet at the very beginning
            _buffer.InsertRange(0, BitConverter.GetBytes(_buffer.Count));
        }

        public void InsertInt(int value)
        {
            // Insert the int at the start of the buffer
            _buffer.InsertRange(0, BitConverter.GetBytes(value));
        }

        public byte[] ToArray()
        {
            _readableBuffer = [.. _buffer];
            return _readableBuffer;
        }

        public void Reset(bool _shouldReset = true)
        {
            if (_shouldReset)
            {
                _buffer.Clear();
                _readableBuffer = [];
                _readPos = 0;
            }
            else
            {
                _readPos -= 4; // "Unread" the last read int
            }
        }
        #endregion

        #region Write Data
        public void Write(byte value) { _buffer.Add(value); }
        public void Write(byte[] value) { _buffer.AddRange(value); }
        public void Write(short value) { _buffer.AddRange(BitConverter.GetBytes(value)); }
        public void Write(int value) { _buffer.AddRange(BitConverter.GetBytes(value)); }
        public void Write(long value) { _buffer.AddRange(BitConverter.GetBytes(value)); }
        public void Write(float value) { _buffer.AddRange(BitConverter.GetBytes(value)); }
        public void Write(bool value) { _buffer.AddRange(BitConverter.GetBytes(value)); }
        public void Write(string value)
        {
            Write(value.Length);
            _buffer.AddRange(Encoding.ASCII.GetBytes(value));
        }
        #endregion

        #region Read Data
        public byte ReadByte(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                byte value = _readableBuffer[_readPos];
                if (moveReadPos)
                {
                    _readPos += 1;
                }
                return value;
            }
            else
            {
                throw new Exception("Could not read value of type 'byte'");
            }
        }

        public byte[] ReadBytes(int length, bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                byte[] value = [.. _buffer.GetRange(_readPos, length)];
                if (moveReadPos)
                {
                    _readPos += length;
                }
                return value;
            }
            else
            {
                throw new Exception("Could not read value of type 'byte[]'");
            }
        }

        public short ReadShort(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                short value = BitConverter.ToInt16(_readableBuffer, _readPos);
                if (moveReadPos)
                {
                    _readPos += 2;
                }
                return value;
            }
            else
            {
                throw new Exception("Could not read value of type 'short'");
            }
        }

        public int ReadInt(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                int value = BitConverter.ToInt32(_readableBuffer, _readPos);
                if (moveReadPos)
                {
                    _readPos += 4;
                }
                return value;
            }
            else
            {
                throw new Exception("Could not read value of type 'int'");
            }
        }

        public long ReadLong(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                long value = BitConverter.ToInt64(_readableBuffer, _readPos);
                if (moveReadPos)
                {
                    _readPos += 8;
                }
                return value;
            }
            else
            {
                throw new Exception("Could not read value of type 'long'");
            }
        }

        public float ReadFloat(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                float value = BitConverter.ToSingle(_readableBuffer, _readPos);
                if (moveReadPos)
                {
                    _readPos += 4;
                }
                return value;
            }
            else
            {
                throw new Exception("Could not read value of type 'float'");
            }
        }

        public bool ReadBool(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                bool value = BitConverter.ToBoolean(_readableBuffer, _readPos);
                if (moveReadPos)
                {
                    _readPos += 1;
                }
                return value;
            }
            else
            {
                throw new Exception("Could not read value of type 'bool'");
            }
        }

        public string ReadString(bool moveReadPos = true)
        {
            try
            {
                int length = ReadInt();
                string value = Encoding.ASCII.GetString(_readableBuffer, _readPos, length);
                if (moveReadPos && value.Length > 0)
                {
                    _readPos += length;
                }
                return value;
            }
            catch
            {
                throw new Exception("Could not read value of type 'string'");
            }
        }
        #endregion

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _buffer = [];
                    _readableBuffer = [];
                    _readPos = 0;
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
