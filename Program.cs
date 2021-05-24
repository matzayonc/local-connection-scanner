using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace CheckIP
{
    class Program
    {
        static void Main(string[] args)
        {
            IPGlobalProperties propertieslP = IPGlobalProperties.GetIPGlobalProperties();
            Console.WriteLine("Nazwa hosta: " + propertieslP.HostName);
            Console.WriteLine("Nazwa domeny: " + propertieslP.DomainName);
            Console.WriteLine();

            int counter = 0;
            foreach (NetworkInterface netCards in NetworkInterface.GetAllNetworkInterfaces())
            {
                Console.WriteLine("Kart #" + ++counter + ": " + netCards.Id);
                Console.WriteLine(" Adres MAC: " + netCards.GetPhysicalAddress().ToString());
                Console.WriteLine(" Opis: " + netCards.Description);
                Console.WriteLine(" Status: " + netCards.OperationalStatus);
                Console.WriteLine(" Szybkoge.: " + (netCards.Speed) / (double)1000000 + " Mb/s");
                Console.WriteLine(" Adresy bram sieciowych: ");

                foreach (GatewayIPAddressInformation gateAddress in netCards.GetIPProperties().GatewayAddresses)
                    Console.WriteLine(" " + gateAddress.Address.ToString());

                Console.WriteLine(" Serwery DNS: ");
                foreach (IPAddress addressIP in netCards.GetIPProperties().DnsAddresses)
                    Console.WriteLine(" " + addressIP.ToString());

                Console.WriteLine(" Serwery DHCP: ");
                foreach (IPAddress addressIP in netCards.GetIPProperties().DhcpServerAddresses)
                    Console.WriteLine(" " + addressIP.ToString());

                Console.WriteLine(" Serwery WINS: ");
                foreach (IPAddress addressIP in netCards.GetIPProperties().WinsServersAddresses)
                    Console.WriteLine(" " + addressIP.ToString());

                Console.WriteLine();


            }


            Console.WriteLine("Aktualne połączenia TCP/IP typu klient: ");
            foreach (TcpConnectionInformation connectTcp in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections())
            {
                Console.WriteLine(" Zdalny adres: " + connectTcp.RemoteEndPoint.Address.ToString() + ":" + connectTcp.RemoteEndPoint.Port);
                Console.WriteLine(" Satatus: " + connectTcp.State.ToString());
            }

            Console.WriteLine("Aktualne połączenia TCP/IP typu serwer: ");
            foreach (IPEndPoint connectTcp in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners())
                Console.WriteLine(" Zdalny adres: " + connectTcp.Address.ToString() + ":" + connectTcp.Port);

            Console.WriteLine("Aktualne połączenia UDP: ");
            foreach (IPEndPoint connectUdp in IPGlobalProperties.GetIPGlobalProperties().GetActiveUdpListeners())
                Console.WriteLine(" Zdalny adres: " + connectUdp.Address.ToString() + ":" + connectUdp.Port);


            Console.ReadKey();
        }
    }
}
