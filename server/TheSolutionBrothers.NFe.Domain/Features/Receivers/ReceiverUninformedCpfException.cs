using NDD.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDD.NFe.Domain.Features.Receivers
{
	public class ReceiverUninformedCpfException : BusinessException
	{
		public ReceiverUninformedCpfException() : base("Cpf não informado.")
		{
		}
	}
}
