using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleClientNetworkingProject
{
    class Program
    {
        //You can adapt the code below for communications on port 3461 and 3462.
        static void SynchronousConnection(string address, int port)
        {
            try
            {
                TcpClient client = new TcpClient(address, port);

                //A using statement should automatically flush when it goes out of scope
                using(BufferedStream stream = new BufferedStream(client.GetStream()))
                {
                    BinaryReader reader = new BinaryReader(stream);
                    BinaryWriter writer = new BinaryWriter(stream);

                    //TODO: do work!
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("error: {0}", ex.Message);
                throw ex;
            }
        }

        //A continuous connection is the best approach for communication on 3463
        public static void ContinuousConnection(string address, int port)
        {
            TcpClient client;
            BufferedStream stream;
            BinaryReader reader;
            BinaryWriter writer;
            try
            {
                client = new TcpClient(address, port);
                stream = new BufferedStream(client.GetStream());
                reader = new BinaryReader(stream);
                writer = new BinaryWriter(stream);

                //if you don't use a using statement, you'll need to flush manually.
                stream.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: {0}", ex.Message);
                throw ex;
            }
            while(true)
            {
                //TODO: do work!
            }
        }

        static async Task TaskedConnection(string address, int port)
        {
            await Task.Run(() => { SynchronousConnection(address, port); });
        }

        public static void ThreadedConnection(string address, int port)
        {
            ThreadStart ts = () => { ContinuousConnection(address, port); } ;
            Thread thread = new Thread(ts);
            thread.Start();

            //if you want to block until the thread is done, call join.  Otherwise, you can
            //just return
            //thread.Join();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
