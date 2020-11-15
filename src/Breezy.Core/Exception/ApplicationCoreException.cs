///////////////////////////////////////////////////////////////////////////////////////////////////
// This file is part of the Breezy project (https://github.com/griffinplus/breezy)
//
// Copyright (c) 2020 Sascha Falk <sascha@falk-online.eu>
//
// The source code is licensed under the AGPL-3.0 license.
//
// This code is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License, version 3,
// as published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License, version 3,
// along with this program. If not, see <http://www.gnu.org/licenses/>
///////////////////////////////////////////////////////////////////////////////////////////////////

using System;

namespace GriffinPlus.Breezy
{
	/// <summary>
	/// Base class for exceptions that are associated with an exit code the application should terminate with.
	/// </summary>
	public class ApplicationCoreException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationCoreException"/> class.
		/// </summary>
		/// <param name="exitCode">Exit code the application should terminate with.</param>
		/// <param name="message">Message describing the reason why the exception is thrown.</param>
		public ApplicationCoreException(ExitCode exitCode, string message) :
			base(message)
		{
			ExitCode = exitCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationCoreException"/> class.
		/// </summary>
		/// <param name="exitCode">Exit code the application should terminate with.</param>
		/// <param name="message">Message describing the reason why the exception is thrown.</param>
		/// <param name="innerException">Exception that was the reason why the exception is thrown.</param>
		public ApplicationCoreException(ExitCode exitCode, string message, Exception innerException) :
			base(message, innerException)
		{
			ExitCode = exitCode;
		}

		/// <summary>
		/// Gets the exit code the application should terminate with.
		/// </summary>
		public ExitCode ExitCode { get; }
	}
}
