﻿// <copyright file="PbszCommandHandler.cs" company="Fubar Development Junker">
// Copyright (c) Fubar Development Junker. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FubarDev.FtpServer.CommandHandlers;

namespace FubarDev.FtpServer.AuthTls
{
    /// <summary>
    /// The <code>PBSZ</code> command handler
    /// </summary>
    public class PbszCommandHandler : FtpCommandHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PbszCommandHandler"/> class.
        /// </summary>
        /// <param name="connection">The connection to create this command handler for</param>
        public PbszCommandHandler(FtpConnection connection)
            : base(connection, "PBSZ")
        {
        }

        /// <inheritdoc/>
        public override bool IsLoginRequired => false;

        /// <inheritdoc/>
        public override IEnumerable<IFeatureInfo> GetSupportedFeatures()
        {
            if (AuthTlsCommandHandler.ServerCertificate != null)
                yield return new GenericFeatureInfo("PBSZ");
        }

        /// <inheritdoc/>
        public override Task<FtpResponse> Process(FtpCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.Argument))
                return Task.FromResult(new FtpResponse(501, "Protection buffer size not specified."));
            var bufferSize = Convert.ToInt32(command.Argument, 10);
            if (bufferSize != 0)
                return Task.FromResult(new FtpResponse(501, "A protection buffer size other than 0 is not supported."));
            return Task.FromResult(new FtpResponse(200, $"Protection buffer size set to {bufferSize}."));
        }
    }
}
