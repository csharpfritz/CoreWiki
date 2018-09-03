using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWiki.Application.Articles.Commands
{
	public class IncrementViewCount : IRequest
	{

		public IncrementViewCount(string slug)
		{
			this.Slug = slug;
		}

		public string Slug { get; }

	}
}
