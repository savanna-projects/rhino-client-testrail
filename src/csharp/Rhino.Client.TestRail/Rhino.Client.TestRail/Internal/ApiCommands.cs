/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/start
 */
namespace Rhino.Client.TestRail.Internal
{
    public static class ApiCommands
    {
        private const string API = "index.php?/api/v2";

        #region *** controller: attachments   ***
        // POST index.php?/api/v2/add_attachment_to_result/:result_id
        /// <summary>
        /// Adds attachment to a result based on the result ID. The maximum allowable upload size is set to 256mb.
        /// </summary>
        public const string ADD_ATTACHMENT_TO_RESULT = API + "/add_attachment_to_result/{0}";

        // POST index.php?/api/v2/add_attachment_to_result_for_case/:result_id/:case_id
        /// <summary>
        /// Adds attachment to a result based on a combination of result and test case IDs.
        /// </summary>
        public const string ADD_ATTACHMENT_TO_RESULT_FOR_CASE = API + "/add_attachment_to_result_for_case/{0}/{1}";

        // GET index.php?/api/v2/get_attachments_for_case/:case_id
        /// <summary>
        /// Returns a list of attachments for a test case.
        /// </summary>
        public const string GET_ATTACHMENTS_FOR_CASE = API + "/get_attachments_for_case/{0}";

        // GET index.php?/api/v2/get_attachments_for_test/:test_id
        /// <summary>
        /// Returns a list of attachments for test results.
        /// </summary>
        public const string GET_ATTACHMENTS_FOR_TEST = API + "/get_attachments_for_test/{0}";

        // GET index.php?/api/v2/get_attachment/:attachment_id
        /// <summary>
        /// Returns the requested attachment identified by :attachment_id.
        /// </summary>
        public const string GET_ATTACHMENT = API + "/get_attachment/{0}";

        // POST index.php?/api/v2/delete_attachment/:attachment_id
        /// <summary>
        /// Deletes the specified attachment identified by :attachment_id.
        /// </summary>
        public const string DELETE_ATTACHMENT = API + "/delete_attachment/{0}";
        #endregion

        #region *** controller: cases         ***
        // GET index.php?/api/v2/get_test/:test_id
        /// <summary>
        /// returns an existing test
        /// </summary>
        public const string GET_CASE = API + "/get_case/{0}";

        // GET index.php?/api/v2/get_cases/:project_id
        /// <summary>
        /// returns a list of test cases for a test suite or specific section in a test suite
        /// </summary>
        public const string GET_CASE_PROJECT = API + "/get_cases/{0}";

        // GET index.php?/api/v2/get_cases/:project_id&suite_id=:suite_id
        /// <summary>
        /// returns a list of test cases for a test suite or specific section in a test suite
        /// </summary>
        public const string GET_CASE_PROJECT_SUITE = API + "/get_cases/{0}&suite_id={1}";

        // POST index.php?/api/v2/add_case/:section_id
        /// <summary>
        /// Creates a new test case.
        /// </summary>
        public const string ADD_CASE = API + "/add_case/{0}";

        // POST index.php?/api/v2/update_case/:case_id
        /// <summary>
        /// Updates an existing test case (partial updates are supported, 
        /// i.e. you can submit and update specific fields only).
        /// </summary>
        public const string UPDATE_CASE = API + "/update_case/{0}";

        // POST index.php?/api/v2/delete_case/:case_id
        /// <summary>
        /// Deletes an existing test case.
        /// </summary>
        public const string DELETE_CASE = API + "/delete_case/{0}";
        #endregion

        #region *** controller: configuration ***
        // GET index.php?/api/v2/get_configs/:project_id
        /// <summary>
        /// returns a list of available configurations, grouped by configuration groups (requires TestRail 3.1 or later)
        /// </summary>
        public const string GET_CONFIGS = API + "/get_configs/{0}";

        // POST index.php?/api/v2/add_config_group/:project_id
        /// <summary>
        /// creates a new configuration group (requires TestRail 5.2 or later)
        /// </summary>
        public const string ADD_CONFIG_GROUP = API + "/add_config_group/{0}";

        // POST index.php?/api/v2/add_config/:config_group_id
        /// <summary>
        /// creates a new configuration (requires TestRail 5.2 or later)
        /// </summary>
        public const string ADD_CONFIG = API + "/add_config/{0}";

        // POST index.php?/api/v2/update_config_group/:config_group_id
        /// <summary>
        /// Updates an existing configuration group (requires TestRail 5.2 or later).
        /// </summary>
        public const string UPDATE_CONFIG_GROUP = API + "/update_config_group/{0}";

        // POST index.php?/api/v2/update_config/:config_id
        /// <summary>
        /// Updates an existing configuration (requires TestRail 5.2 or later).
        /// </summary>
        public const string UPDATE_CONFIG = API + "/update_config/{0}";

        // POST index.php?/api/v2/delete_config_group/:config_group_id
        /// <summary>
        /// Deletes an existing configuration group and its configurations (requires TestRail 5.2 or later).
        /// </summary>
        public const string DELETE_CONFIG_GROUP = API + "/delete_config_group/{0}";

        // POST index.php?/api/v2/delete_config/:config_id
        /// <summary>
        /// Deletes an existing configuration (requires TestRail 5.2 or later).
        /// </summary>
        public const string DELETE_CONFIG = API + "/delete_config/{0}";
        #endregion

        #region *** controller: milestones    ***
        // GET index.php?/api/v2/get_milestone/:milestone_id
        /// <summary>
        /// Returns an existing milestone.
        /// </summary>
        public const string GET_MILESTONE = API + "/get_milestone/{0}";

        // GET index.php?/api/v2/get_milestones/:project_id
        /// <summary>
        /// Returns the list of milestones for a project.
        /// </summary>
        public const string GET_MILESTONES = API + "/get_milestones/{0}";

        // POST index.php?/api/v2/add_milestone/:project_id
        /// <summary>
        /// Creates a new milestone.
        /// </summary>
        public const string ADD_MILESTONE = API + "/add_milestone/{0}";

        // POST index.php?/api/v2/update_milestone/:milestone_id
        /// <summary>
        /// Updates an existing milestone (partial updates are supported, i.e. you can submit and update specific fields only).
        /// </summary>
        public const string UPDATE_MILESTONE = API + "/update_milestone/{0}";

        // POST index.php?/api/v2/delete_milestone/:milestone_id
        /// <summary>
        /// Deletes an existing milestone.
        /// </summary>
        public const string DELETE_MILESTONE = API + "/delete_milestone/{0}";
        #endregion

        #region *** controller: plans         ***
        // GET index.php?/api/v2/get_plan/:plan_id
        /// <summary>
        /// Returns an existing test plan.
        /// </summary>
        public const string GET_PLAN = API + "/get_plan/{0}";

        // GET index.php?/api/v2/get_plans/:project_id
        /// <summary>
        /// Returns a list of test plans for a project.
        /// </summary>
        public const string GET_PLANS = API + "/get_plans/{0}";

        // GET index.php?/api/v2/get_plans/:project_id?offset=20
        /// <summary>
        /// Returns a list of test plans for a project. Use :offset to skip records.
        /// </summary>
        public const string GET_PLANS_OFFSET = API + "/get_plans/{0}?offset={1}";

        // POST index.php?/api/v2/add_plan/:project_id
        /// <summary>
        /// Creates a new test plan.
        /// </summary>
        public const string ADD_PLAN = API + "/add_plan/{0}";

        // POST index.php?/api/v2/add_plan_entry/:plan_id
        /// <summary>
        /// Adds one or more new test runs to a test plan.
        /// </summary>
        public const string ADD_PLAN_ENTRY = API + "/add_plan_entry/{0}";

        // POST index.php?/api/v2/update_plan/:plan_id
        /// <summary>
        /// Updates an existing test plan (partial updates are supported, i.e. 
        /// you can submit and update specific fields only).
        /// </summary>
        public const string UPDATE_PLAN = API + "/update_plan/{0}";

        // POST index.php?/api/v2/update_plan_entry/:plan_id/:entry_id
        /// <summary>
        /// Updates one or more existing test runs in a plan (partial updates are supported, i.e. 
        /// you can submit and update specific fields only).
        /// </summary>
        public const string UPDATE_PLAN_ENTRY = API + "/update_plan_entry/{0}/{1}";

        // POST index.php?/api/v2/close_plan/:plan_id
        /// <summary>
        /// Closes an existing test plan and archives its test runs & results.
        /// </summary>
        public const string CLOSE_PLAN = API + "/close_plan/{0}";

        // POST index.php?/api/v2/delete_plan/:plan_id
        /// <summary>
        /// Deletes an existing test plan.
        /// </summary>
        public const string DELETE_PLAN = API + "/delete_plan/{0}";

        // POST index.php?/api/v2/delete_plan_entry/:plan_id/:entry_id
        /// <summary>
        /// Deletes one or more existing test runs from a plan.
        /// </summary>
        public const string DELETE_PLAN_ENTRY = API + "/delete_plan_entry/{0}/{1}";
        #endregion

        #region *** controller: projects      ***
        // GET index.php?/api/v2/get_project/:project_id
        /// <summary>
        /// Returns an existing project.
        /// </summary>
        public const string GET_PROJECT = API + "/get_project/{0}";

        // GET index.php?/api/v2/get_projects
        /// <summary>
        /// Returns the list of available projects.
        /// </summary>
        public const string GET_PROJECTS = API + "/get_projects";

        // POST index.php?/api/v2/add_project
        /// <summary>
        /// Creates a new project (administrator status required).
        /// </summary>
        public const string ADD_PROJECT = API + "/v2/add_project";

        // POST index.php?/api/v2/update_project/:project_id
        /// <summary>
        /// Updates an existing project (administrator status required; partial updates are supported, i.e. 
        /// you can submit and update specific fields only).
        /// </summary>
        public const string UPDATE_PROJECT = API + "/update_project/{0}";

        // POST index.php?/api/v2/delete_project/:project_id
        /// <summary>
        /// Deletes an existing project (administrator status required).
        /// </summary>
        public const string DELETE_PROJECT = API + "/delete_project/{0}";
        #endregion

        #region *** controller: results       ***
        // GET index.php?/api/v2/get_results/:test_id
        /// <summary>
        /// Returns a list of test results for a test (up to 250).
        /// </summary>
        public const string GET_RESULTS = API + "/get_results/{0}";

        // GET index.php?/api/v2/get_results/:test_id&offset=:offset
        /// <summary>
        /// Returns a list of test results for a test (up to 250).
        /// </summary>
        public const string GET_RESULTS_OFFSET = API + "/get_results/{0}&offset={1}";

        // GET index.php?/api/v2/get_results/:test_id&status_id=:status_id
        /// <summary>
        /// Returns a list of test results for a test (up to 250).
        /// </summary>
        public const string GET_RESULTS_STATUS = API + "/get_results/{0}&status_id={1}";

        // GET index.php?/api/v2/get_results/:test_id&offset=:offset&status_id=:status_id
        /// <summary>
        /// Returns a list of test results for a test (up to 250).
        /// </summary>
        public const string GET_RESULTS_OFFSET_STATUS = API + "/get_results/{0}&offset={1}&status_id={2}";

        // GET index.php?/api/v2/get_results_for_case/:run_id/:case_id
        /// <summary>
        /// Returns a list of test results for a test run and case combination.
        /// </summary>
        public const string GET_RESUTLS_FOR_CASE = API + "/get_results_for_case/{0}/{1}";

        // GET index.php?/api/v2/get_results_for_run/:run_id
        /// <summary>
        /// Returns a list of test results for a test run.
        /// </summary>
        public const string GET_RESUTLS_FOR_RUN = API + "/get_results_for_run/{0}";

        // POST index.php?/api/v2/add_result/:test_id
        /// <summary>
        /// Adds a new test result, comment or assigns a test.
        /// </summary>
        public const string ADD_RESULT = API + "/add_result/{0}";

        // POST index.php?/api/v2/add_result_for_case/:run_id/:case_id
        /// <summary>
        /// Adds a new test result, comment or assigns a test (for a test run and case combination).
        /// It's recommended to use add_results_for_cases instead if you plan to add results for multiple test cases.
        /// </summary>
        public const string ADD_RESULT_FOR_CASE = API + "/add_result_for_case/{0}/{1}";

        // POST index.php?/api/v2/add_results/:run_id
        /// <summary>
        /// Adds one or more new test results, comments or assigns one or more tests.
        /// Ideal for test automation to bulk-add multiple test results in one step.
        /// </summary>
        public const string ADD_RESULTS = API + "/add_results/{0}";

        // POST index.php?/api/v2/add_results_for_cases/:run_id
        /// <summary>
        /// Adds one or more new test results, comments or assigns one or more tests (using the case IDs).
        /// Ideal for test automation to bulk-add multiple test results in one step.
        /// </summary>
        public const string ADD_RESULTS_FOR_CASES = API + "/add_results_for_cases/{0}";
        #endregion

        #region *** controller: runs          ***
        // GET index.php?/api/v2/get_run/:run_id
        /// <summary>
        /// Returns an existing test run. Please see get_tests for the list of included tests in this run.
        /// </summary>
        public const string GET_RUN = API + "/get_run/:{0}";

        // GET index.php?/api/v2/get_runs/:project_id
        /// <summary>
        /// returns a list of test runs for a project. only returns those test runs that are not part 
        /// of a test plan (please see get_plans/get_plan for this)
        /// </summary>
        public const string GET_RUNS = API + "/get_runs/{0}";

        // GET index.php?/api/v2/get_runs/:project_id&offset=:offset
        /// <summary>
        /// returns a list of test runs for a project. only returns those test runs that are not part 
        /// of a test plan (please see get_plans/get_plan for this)
        /// </summary>
        public const string GET_RUNS_OFFSET = API + "/get_runs/{0}?offset={1}";

        // GET index.php?/api/v2/get_runs/:project_id&suite_id=:suite_id
        /// <summary>
        /// returns a list of test runs for a project. only returns those test runs that are not part 
        /// of a test plan (please see get_plans/get_plan for this)
        /// </summary>
        public const string GET_RUNS_SUITE_ID = API + "/get_runs/{0}?suite_id={1}";

        // POST index.php?/api/v2/add_run/:project_id
        /// <summary>
        /// Creates a new test run.
        /// </summary>
        public const string ADD_RUN = API + "/add_run/{0}";

        // POST index.php?/api/v2/update_run/:run_id
        /// <summary>
        /// Updates an existing test run (partial updates are supported, i.e. 
        /// you can submit and update specific fields only).
        /// </summary>
        public const string UPDATE_RUN = API + "/update_run/{0}";

        // POST index.php?/api/v2/close_run/:run_id
        /// <summary>
        /// Closes an existing test run and archives its tests & results.
        /// </summary>
        public const string CLOSE_RUN = API + "/close_run/{0}";

        // POST index.php?/api/v2/delete_run/:run_id
        /// <summary>
        /// Deletes an existing test run.
        /// </summary>
        public const string DELETE_RUN = API + "/delete_run/{0}";
        #endregion

        #region *** controller: suites        ***
        // GET index.php?/api/v2/get_suite/:suite_id
        /// <summary>
        /// Returns an existing test suite.
        /// </summary>
        public const string GET_SUITE = API + "/get_suite/{0}";

        // GET index.php?/api/v2/get_suites/:project_id
        /// <summary>
        /// Returns a list of test suites for a project.
        /// </summary>
        public const string GET_SUITES = API + "/get_suites/{0}";

        // POST index.php?/api/v2/add_suite/:project_id
        /// <summary>
        /// Creates a new test suite.
        /// </summary>
        public const string ADD_SUITE = API + "/add_suite/{0}";

        // POST index.php?/api/v2/update_suite/:suite_id
        /// <summary>
        /// Updates an existing test suite (partial updates are supported, i.e. 
        /// you can submit and update specific fields only).
        /// </summary>
        public const string UPDATE_SUITE = API + "/update_suite/{0}";

        // POST index.php?/api/v2/delete_suite/:suite_id
        /// <summary>
        /// Deletes an existing test suite.
        /// </summary>
        public const string DELETE_SUITE = API + "/delete_suite/{0}";
        #endregion

        #region *** controller: tests         ***
        // GET index.php?/api/v2/get_test/:test_id
        /// <summary>
        /// Returns an existing test.
        /// </summary>
        public const string GET_TEST = API + "/get_test/{0}";

        // GET index.php?/api/v2/get_tests/:run_id
        /// <summary>
        /// Returns a list of tests for a test run.
        /// </summary>
        public const string GET_TESTS = API + "/get_tests/{0}";
        #endregion

        #region *** controller: users         ***
        // GET index.php?/api/v2/get_user/:user_id
        /// <summary>
        /// Returns an existing user.
        /// </summary>
        public const string GET_USER = API + "/get_user/{0}";

        // GET index.php?/api/v2/get_user_by_email&email=:email
        /// <summary>
        /// Returns an existing user by his/her email address.
        /// </summary>
        public const string GET_USER_BY_EMAIL = API + "/get_user_by_email&email={0}";

        // GET index.php?/api/v2/get_users
        /// <summary>
        /// Returns a list of users.
        /// </summary>
        public const string GET_USERS = API + "/get_users";
        #endregion
    }
}