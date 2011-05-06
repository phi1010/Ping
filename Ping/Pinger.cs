bool checkconnection = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

public static bool CheckInternetConnection()
{
	Ping ping = new Ping();

	try
	{
		PingReply reply = ping.Send("www.google.at", 100);

		return reply.Status == IPStatus.Success;
	}
	catch
	{
		return false;
	}
}