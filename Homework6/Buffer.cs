using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework6
{
    // Source buffer class
    class SourceBuffer
    {
        // The byte array
        private byte[] arr;
        int count;
        int diskSize;

        // Path of the source file
        private string path;
        // Size of the source file
        private int fileSize;
        // Size of the data read
        private int dataSize;

        // Constructor
        public SourceBuffer(int size, string path)
        {
            // Initialize the array
            arr = new byte[size];
            count = diskSize = size;
            // Initialize information of the source file
            this.path = path;
            FileInfo info = new FileInfo(path);
            this.fileSize = Convert.ToInt32(info.Length);
            this.dataSize = 0;
        }

        // Check if the file is completely read
        public bool IsCompletelyRead()
        {
            if (dataSize >= fileSize && count == arr.Length)
                return true;
            return false;
        }

        // Set the buffer to a position of the file
        public void SetTo(int position)
        {
            if (arr.Length < diskSize)
                arr = new byte[diskSize];
            count = diskSize;
            dataSize = position;
        }

        // Get integer from buffer
        public int GetInt()
        {
            if (arr.Length - count >= 4)
            {
                int value = BitConverter.ToInt32(arr, count);
                count += 4;
                return value;
            }

            byte[] res = new byte[4];
            for (int i = 0; i < 4; ++i)
            {
                if (count == arr.Length)
                    ReadFromDisk();
                res[i] = arr[count];
                ++count;
            }
            return BitConverter.ToInt32(res, 0);
        }

        // Get double from buffer
        public double GetDouble()
        {
            if (arr.Length - count >= 8)
            {
                double value = BitConverter.ToDouble(arr, count);
                count += 8;
                return value;
            }

            byte[] res = new byte[8];
            for (int i = 0; i < 8; ++i)
            {
                if (count == arr.Length)
                    ReadFromDisk();
                res[i] = arr[count];
                ++count;
            }
            return BitConverter.ToDouble(res, 0);
        }

        // Get string from buffer
        public string GetString(int len)
        {
            len <<= 1;
            string value = "";
            if (arr.Length - count >= len)
            {
                for (int i = 0; i < len; i += 2)
                {
                    value += BitConverter.ToChar(arr, count);
                    count += 2;
                }
                return value;
            }

            byte[] temp = new byte[2];
            for (int i = 0; i < len; ++i)
            {
                if (count == arr.Length)
                    ReadFromDisk();
                temp[i % 2] = arr[count];
                if (i % 2 == 1)
                    value += BitConverter.ToChar(temp, 0);
                ++count;
            }
            return value;
        }

        // Get bytes from buffer
        public byte[] GetBytes(int len)
        {
            if (len <= 0)
                return new byte[0];
            byte[] res = new byte[len];
            for (int i = 0; i < len; ++i)
            {
                if (count == arr.Length)
                    ReadFromDisk();
                res[i] = arr[count];
                ++count;
            }
            return res;
        }

        // Fill buffer when it is empty
        private void ReadFromDisk()
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                reader.BaseStream.Position = dataSize;
                int size = reader.Read(arr, 0, arr.Length);
                if (size < arr.Length)
                    Array.Resize(ref arr, size);
                count = 0;
                dataSize += arr.Length;
            }
        }
    }

    // Destination buffer class
    class DestinationBuffer
    {
        // The byte array
        private byte[] arr;
        int count;

        // Path of the file to write when buffer is full
        private string path;

        // Constructor
        public DestinationBuffer(int size, string path)
        {
            arr = new byte[size];
            count = 0;
            this.path = path;
        }

        // Store a byte array to buffer
        public void Store(byte[] data)
        {
            for (int i = 0; i < data.Length; ++i)
            {
                arr[count] = data[i];
                ++count;
                if (count == arr.Length)
                    WriteToDisk();
            }
        }

        // Store a part of the byte array to buffer
        public void Store(byte[] data, int start, int end)
        {
            for (int i = start; i < end; ++i)
            {
                arr[count] = data[i];
                ++count;
                if (count == arr.Length)
                    WriteToDisk();
            }
        }

        // Write buffer to disk when it is full
        private void WriteToDisk()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Append)))
            {
                writer.Write(arr);
                count = 0;
            }
        }

        // Write buffer to disk on request
        public void WriteToDiskOnRequest()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Append)))
            {
                writer.Write(arr.Take(count).ToArray());
                count = 0;
            }
        }
    }
}
