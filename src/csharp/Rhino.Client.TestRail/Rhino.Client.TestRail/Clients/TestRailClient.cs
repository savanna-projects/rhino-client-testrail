/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/start
 */
using Rhino.Client.TestRail.Contracts;
using Rhino.Client.TestRail.Extensions;
using Rhino.Client.TestRail.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Rhino.Client.TestRail.Clients
{
    /// <summary>
    /// Use TestRail's API to integrate automated tests, submit test results and automate various aspects of TestRail
    /// </summary>
    public abstract class TestRailClient
    {
        // constants
        internal const StringComparison COMPARE = StringComparison.OrdinalIgnoreCase;

        // constants: logger
        internal const string COMMAND_MESSAGE = "command [{0}] executed successfully";
        internal const string APPLICATION = "automation.test-rail.client";

        // members: state
        internal readonly Uri testRailServer;
        private readonly string user;
        private readonly string password;

        #region *** constructors    ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        protected TestRailClient(string testRailServer, string user, string password)
            : this(new Uri(testRailServer), user, password, Utilities.GetTraceListener(APPLICATION, $"{Environment.CurrentDirectory}\\logs")) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        protected TestRailClient(Uri testRailServer, string user, string password, TraceListener logger)
        {
            this.testRailServer = testRailServer;
            this.user = user;
            this.password = password;
            JsonSettings = Utilities.GetJsonSettings<SnakeCaseNamingStrategy>();
            Projects = GetProjects();
            Trace.Listeners.AddIfNotExists(logger, APPLICATION);
        }
        #endregion

        #region *** properties      ***
        /// <summary>
        /// gets the projects collection of this server
        /// </summary>
        internal IEnumerable<TestRailProject> Projects { get;}

        /// <summary>
        /// gets the this json setting (snake case resolver)
        /// </summary>
        internal JsonSerializerSettings JsonSettings { get; }
        #endregion

        #region *** executors: post ***
        /// <summary>
        /// Send a POST request to the specified Uri
        /// </summary>
        /// <typeparam name="T">Type of the expected result object</typeparam>
        /// <param name="command">The Uri the request is sent to</param>
        /// <param name="data">The HTTP request content sent to the server</param>
        /// <returns>Data Transfer Object(s) (DTO) of the requests type</returns>
        internal T ExecutePost<T>(string command, object data) => Post<T>(command, data);

        /// <summary>
        /// Send a POST request to the specified Uri
        /// </summary>
        /// <typeparam name="T">Type of the expected result object</typeparam>
        /// <param name="command">The Uri the request is sent to</param>
        /// <returns>Data Transfer Object(s) (DTO) of the requests type</returns>
        internal T ExecutePost<T>(string command) => Post<T>(command, default);

        /// <summary>
        /// Send a POST request to the specified Uri
        /// </summary>
        /// <param name="command">The Uri the request is sent to</param>
        /// <param name="data">The HTTP request content sent to the server</param>
        internal void ExecutePost(string command, object data) => Post(command, data);

        /// <summary>
        /// Send a POST request to the specified Uri
        /// </summary>
        /// <param name="command">The Uri the request is sent to</param>
        internal void ExecutePost(string command) => Post(command, default);

        private T Post<T>(string command, object data)
        {
            // create command
            var httpCommand = new TestRailHttpCommand
            {
                Endpoint = command,
                HttpMethod = HttpMethod.Post.Method,
                Data = data
            };

            // execute command
            var entities = HttpExecutor<T>(httpCommand);
            Trace.TraceInformation(string.Format(COMMAND_MESSAGE, httpCommand?.Endpoint));

            // return entity
            return entities;
        }

        private void Post(string command, object data)
        {
            // create command
            var httpCommand = new TestRailHttpCommand
            {
                Endpoint = command,
                HttpMethod = HttpMethod.Post.Method,
                Data = data
            };

            // execute command
            HttpExecutor(httpCommand);
            Trace.TraceInformation(string.Format(COMMAND_MESSAGE, httpCommand?.Endpoint));
        }
        #endregion

        #region *** executors: get  ***
        /// <summary>
        /// Send a GET request to the specified Uri
        /// </summary>
        /// <typeparam name="T">Type of the expected result object</typeparam>
        /// <param name="command">The Uri the request is sent to</param>
        /// <returns>Data Transfer Object(s) (DTO) of the requests type</returns>
        internal T ExecuteGet<T>(string command)
        {
            // create command
            var httpCommand = new TestRailHttpCommand { Endpoint = command };

            // execute command
            var entities = HttpExecutor<T>(httpCommand);
            Trace.TraceInformation(string.Format(COMMAND_MESSAGE, httpCommand?.Endpoint));

            // return entity
            return entities;
        }
        #endregion

        /// <summary>
        /// Returns an existing project.
        /// </summary>
        /// <param name="project">The name of the project</param>
        /// <returns>Sn existing project</returns>
        internal TestRailProject GetProjectByName(string project)
            => Projects.FirstOrDefault(i => i.Name.Equals(project, COMPARE));

        // gets all projects from the current server connection
        private IEnumerable<TestRailProject> GetProjects()
        {
            // constant: logging
            const string M1 = "caching projects from [{0}]";
            const string M2 = "total of [{0}] project cached";

            Trace.TraceInformation(string.Format(M1, testRailServer?.AbsoluteUri));
            var command = new TestRailHttpCommand { Endpoint = ApiCommands.GET_PROJECTS, HttpMethod = "GET" };
            var responseBody = HttpExecutor(command);
            var projects = JsonConvert.DeserializeObject<TestRailProject[]>(responseBody);

            Trace.TraceInformation(string.Format(M2, projects?.Count()));
            return projects;
        }

        // get a web-request ready for interaction with test-rail server
        private T HttpExecutor<T>(TestRailHttpCommand command)
        {
            // get response body
            var response = GetWebResponse(command);
            var responseBody = response?.ReadBody();

            if (responseBody == null)
            {
                return default;
            }

            // convert to bytes array
            if (typeof(T) == typeof(byte[]))
            {
                var buffer = Encoding.UTF8.GetBytes(responseBody);
                responseBody = JsonConvert.SerializeObject(buffer);
                return JsonConvert.DeserializeObject<T>(responseBody, JsonSettings);
            }

            // process            
            if (typeof(T).IsArray)
            {
                return ProcessArrayResponse<T>(responseBody);
            }
            var responseContract = JsonConvert.DeserializeObject<T>(responseBody, JsonSettings);
            return ProcessCustomFields(responseContract, responseBody);
        }

        // process array response
        private T ProcessArrayResponse<T>(string responseBody)
        {
            // collect contracts
            var contracts = JArray.Parse(responseBody);

            // get element type by which to create single response object
            var elementType = typeof(T).GetElementType();

            // iterate each contract, deserialize, add context & custom fields
            var responseObject = new List<object>();
            foreach (var item in contracts)
            {
                var contract = JsonConvert.DeserializeObject($"{item}", elementType, JsonSettings);
                var i = ProcessCustomFields(contract, $"{item}");
                responseObject.Add(i);
            }
            var transaction = JsonConvert.SerializeObject(responseObject, JsonSettings);
            return JsonConvert.DeserializeObject<T>(transaction, JsonSettings);
        }

        // process custom fields
        private T ProcessCustomFields<T>(T contract, string responseBody)
        {
            // exit conditions
            if (contract.GetType().BaseType != typeof(Contract))
            {
                return contract;
            }

            // process custom fields
            ((IContext)contract).Context.AddOrReplace(nameof(WebResponse), responseBody);
            var token = JToken.Parse($"{ ((IContext)contract).Context["WebResponse"]}");
            var cutomFields = token.AsCustomFields();
            contract.GetType().GetProperty(nameof(Contract.CustomFields)).SetValue(contract, cutomFields);

            return contract;
        }

        // get a web-request ready for interaction with test-rail server
        private string HttpExecutor(TestRailHttpCommand command)
        {
            var response = GetWebResponse(command);
            return response?.ReadBody();
        }

        // gets web-response object
        private WebResponse GetWebResponse(TestRailHttpCommand command)
        {
            // constants
            const BindingFlags BINDINGS = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

            // normalize test-rail command
            if (!testRailServer.AbsoluteUri.EndsWith("/"))
            {
                command.Endpoint = $"{testRailServer.AbsoluteUri}/{command.Endpoint}";
            }
            else
            {
                command.Endpoint = testRailServer.AbsoluteUri + command.Endpoint;
            }

            // get web-request method to invoke
            var method = Array
                .Find(GetType()
                .BaseType
                .GetMethods(BINDINGS), i => i.Name.Equals(command.HttpMethod, COMPARE) && i.ReturnType == typeof(WebRequest));
            var request = (WebRequest)method.Invoke(this, new object[] { command });

            // add credentials & content-type
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}"));
            request.Headers.Add("Authorization", "Basic " + credentials);

            // return normalized response
            try
            {
                return request.GetResponse();
            }
            catch (WebException e)
            {
                Trace.TraceWarning(e?.Response.ReadBody());
            }
            return default;
        }

#pragma warning disable S1144, IDE0051, RCS1213
        // creates a POST web-request
        private WebRequest Post(TestRailHttpCommand command)
        {
            // initialize request
            var webRequest = ByCommand(command);

            // attachment handler
            if (command.Endpoint.Contains("add_attachment"))
            {
                return GetAttachmentRequest(webRequest, command);
            }

            // set request-body
            var jsonBody = JsonConvert.SerializeObject(command.Data, Formatting.None, JsonSettings);
            var requestBody = Encoding.UTF8.GetBytes(jsonBody);
            webRequest.GetRequestStream().Write(requestBody, 0, requestBody.Length);

            // return ready to send request
            return webRequest;
        }

        // creates a GET web-request
        private WebRequest Get(TestRailHttpCommand command) => ByCommand(command);
#pragma warning restore S1144, IDE0051, RCS1213

        // creates web-request by HTTP command
        private WebRequest ByCommand(TestRailHttpCommand command)
        {
            var webRequest = WebRequest.Create(command.Endpoint);
            webRequest.Method = command.HttpMethod;
            webRequest.ContentType = command.ContentType;
            return webRequest;
        }

        private WebRequest GetAttachmentRequest(WebRequest request, TestRailHttpCommand command)
        {
            var boundary = string.Format("{0:N}", Guid.NewGuid());
            var filePath = (string)command.Data;

            request.ContentType = "multipart/form-data; boundary=" + boundary;

            using (var postDataWriter = new StreamWriter(new MemoryStream()))
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                postDataWriter.Write("\r\n--" + boundary + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data; name=\"attachment\";"
                                + "filename=\"{0}\""
                                + "\r\nContent-Type: {1}\r\n\r\n",
                                Path.GetFileName(filePath),
                                Path.GetExtension(filePath));
                postDataWriter.Flush();
                var postDataStream = (MemoryStream)postDataWriter.BaseStream;

                var buffer = new byte[postDataStream.ToArray().Length];
                int bytesRead;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    postDataStream.Write(buffer, 0, bytesRead);
                }

                postDataWriter.Write("\r\n--" + boundary + "--\r\n");
                postDataWriter.Flush();
                request.ContentLength = postDataStream.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    postDataStream.WriteTo(requestStream);
                }
            }
            return request;
        }
    }
}