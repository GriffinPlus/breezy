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
using Xunit;

namespace GriffinPlus.Breezy.Tests
{
	/// <summary>
	/// Unit tests targeting the <see cref="ApplicationCoreException"/> class.
	/// </summary>
	public class ApplicationCoreExceptionTests
	{
		[Theory]
		[InlineData(ExitCode.Success)] // default value of enumeration
		[InlineData(ExitCode.GeneralError)]
		void Create_WithExitCode(ExitCode exitCode)
		{
			const string message = "Demo";
			var exception = new ApplicationCoreException(exitCode, message);
			Assert.Equal(exitCode, exception.ExitCode);
			Assert.Equal(message, exception.Message);
			Assert.Null(exception.InnerException);
		}

		[Theory]
		[InlineData(ExitCode.Success)] // default value of enumeration
		[InlineData(ExitCode.GeneralError)]
		void Create_WithExitCodeAndInnerException(ExitCode exitCode)
		{
			const string message = "Demo";
			Exception innerException = new Exception("Inner Exception");
			var exception = new ApplicationCoreException(exitCode, message, innerException);
			Assert.Equal(exitCode, exception.ExitCode);
			Assert.Equal(message, exception.Message);
			Assert.Same(innerException, exception.InnerException);
		}

	}
}
