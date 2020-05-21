package com.automation.gurock.clients;

import com.automation.gurock.contracts.TestRailHttpCommand;
import com.automation.gurock.extensions.Utilities;
import com.google.gson.Gson;

import java.io.*;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.net.*;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.util.Base64;

/**
 * Use TestRail's API to integrate automated tests, submit test results and automate various aspects of TestRail
 */
public class TestRailClient {

    // constants: HTTP
    public static final String POST_METHOD = "POST";
    public static final String GET_METHOD = "GET";
    public static final String APPLICATION_JSON = "application/json; charset=utf-8";

    // constants: logger
    static final String COMMAND_MESSAGE = "command [{0}] executed successfully";
    static final String APPLICATION = "automation.test-rail.client";

    // members: state
    final URL testRailServer;
    private final Gson gson;
    private final String user;
    private final String password;

    /**
     * Creates a new instance of this client
     *
     * @param testRailServer Test-Rail server to create a client against
     * @param user           Test-Rail user name (account email)
     * @param password       Test-Rail password
     * @throws URISyntaxException When string could not be parsed as a URI reference
     */
    // constructors
    public TestRailClient(String testRailServer, String user, String password) throws MalformedURLException {
        this(new URL(testRailServer), user, password);
    }

    /**
     * Creates a new instance of this client
     *
     * @param testRailServer Test-Rail server to create a client against
     * @param user           Test-Rail user name (account email)
     * @param password       Test-Rail password
     */
    private TestRailClient(URL testRailServer, String user, String password) {
        this.testRailServer = testRailServer;
        this.user = user;
        this.password = password;
        gson = new Gson();
    }

    // get a web-request ready for interaction with test-rail server
    @SuppressWarnings("unchecked")
    public <T> T httpExecutor(Class c, TestRailHttpCommand command)
            throws IllegalAccessException, InvocationTargetException {

        // get response body
        String responseBody = getWebResponse(command);
        if (responseBody.equals("")) {
            return null;
        }

        // convert to bytes array
        if (c == byte[].class) {
            byte[] buffer = responseBody.getBytes(StandardCharsets.UTF_8);
            responseBody = gson.toJson(buffer);
            return (T) gson.fromJson(responseBody, c);
        }

        // process
        if (c.isArray()) {
            // processArrayResponse
        }
        T responseContract = (T) gson.fromJson(responseBody, c);
        // return processCustomFields
        return null;
    }

    private String getWebResponse(TestRailHttpCommand command)
            throws InvocationTargetException, IllegalAccessException {

        // normalize test-rail command
        String a = testRailServer.toString();
        if (!command.getEndpoint().startsWith("/")) {
            command.setEndpoint(testRailServer.toString() + "/" + command.getEndpoint());
        } else {
            command.setEndpoint(testRailServer.toString() + command.getEndpoint());
        }

        // get web-request method to invoke
        Method method = getMethod(command.getHttpMethod().toLowerCase());
        assert method != null;
        HttpURLConnection response = (HttpURLConnection) method.invoke(this, command);

        // return normalized response
        StringBuilder responseBody = new StringBuilder();
        try {
            return Utilities.read(response.getInputStream());
        } catch (Exception e) {
            System.out.println(e.toString());
        }
        return "";
    }

    // creates a POST web-request
    @SuppressWarnings("unused")
    private HttpURLConnection post(TestRailHttpCommand command) throws IOException {
        // setup request
        HttpURLConnection con = (HttpURLConnection) new URL(command.getEndpoint()).openConnection();
        con.setRequestMethod("POST");
        con.setRequestProperty("User-Agent", "Mozilla/5.0");
        con.setRequestProperty("Authorization", "Basic " + new String(Base64.getEncoder().encode((user + ":" + password).getBytes())));
        con.setRequestProperty("Content-Type", "application/json");
        con.setDoOutput(true);

        // write request body
        OutputStream outputStream = con.getOutputStream();
        outputStream.write(new Gson().toJson(command.getData()).getBytes());
        outputStream.flush();
        outputStream.close();

        // get status code
        int responseCode = con.getResponseCode();
        System.out.println("POST Response Code :: " + responseCode);

        // exit conditions
        if (responseCode != HttpURLConnection.HTTP_OK) {
            System.out.println("POST request not worked");
        }
        return con;
    }

    // creates a GET web-request
    @SuppressWarnings("unused")
    private HttpURLConnection get(TestRailHttpCommand command) throws IOException {
        // setup request
        HttpURLConnection con = (HttpURLConnection) new URL(command.getEndpoint()).openConnection();
        con.setRequestMethod("GET");
        con.setRequestProperty("User-Agent", "Mozilla/5.0");
        con.setRequestProperty("Authorization", "Basic " + new String(Base64.getEncoder().encode((user + ":" + password).getBytes())));
        con.setRequestProperty("Content-Type", "application/json");

        // get status code
        int responseCode = con.getResponseCode();
        System.out.println("GET Response Code :: " + responseCode);

        // exit conditions
        if (responseCode != HttpURLConnection.HTTP_OK) {
            System.out.println("GET request not worked");
        }
        return con;
    }

    // get http-method executor
    private Method getMethod(String httpMethod) {
        // get base type
        Class baseType = getClass();

        // collect methods
        Method[] methods = baseType.getDeclaredMethods();
        for (Method m : methods) {
            if (!m.getName().equals(httpMethod)) {
                continue;
            }
            return m;
        }
        return null;
    }
}