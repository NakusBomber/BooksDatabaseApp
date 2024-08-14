using System.Data.Common;

namespace Books.BL.Exceptions;

public class DbCannotConnectException : DbException
{
	public DbCannotConnectException()
	{
	}
}
