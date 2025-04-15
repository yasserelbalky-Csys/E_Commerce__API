﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
	public interface ISessionManager
	{
		void Set<T>(string key, T value);
		T? Get<T>(string key);
		void Remove(string key);
	}
}