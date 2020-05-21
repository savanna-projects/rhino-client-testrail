package com.automation.gurock.test.containers;

import com.automation.gurock.clients.TestRailClient;
import com.automation.gurock.contracts.TestRailHttpCommand;
import com.google.gson.Gson;
import org.testng.annotations.Test;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.lang.reflect.InvocationTargetException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.Base64;

public class SanityTests {
    private static final String USER_AGENT = "Mozilla/5.0";
    private static final String GET_URL = "https://hotelbeds.testrail.io/index.php?/api/v2/get_projects";
    private static final String POST_URL = "http://localhost:9090/SpringMVCExample/home";
    private static final String USER = "eyal.sooliman@lastminutetravel.com:C91a29ab5a";

    @Test
    public void test00() throws IOException, IllegalAccessException, InvocationTargetException {
        TestRailClient client = new TestRailClient("https://hotelbeds.testrail.io/index.php?/api/v2", "eyal.sooliman@lastminutetravel.com", "C91a29ab5a");
        TestRailHttpCommand command = new TestRailHttpCommand().setEndpoint("/get_projects").setHttpMethod("GET");
        client.httpExecutor(command);
    }

    @Test
    public void test01() throws IOException {
        post(new Object()); // POST
        get();              // GET
    }

    private void post(Object data) throws IOException {
        // setup request
        HttpURLConnection con = (HttpURLConnection) new URL(POST_URL).openConnection();
        con.setRequestMethod("POST");
        con.setRequestProperty("User-Agent", USER_AGENT);
        con.setRequestProperty("Authorization", "Basic " + new String(Base64.getEncoder().encode(USER.getBytes())));
        con.setRequestProperty("Content-Type", "application/json");
        con.setDoOutput(true);

        // write request body
        OutputStream outputStream = con.getOutputStream();
        outputStream.write(new Gson().toJson(data).getBytes());
        outputStream.flush();
        outputStream.close();

        // get status code
        int responseCode = con.getResponseCode();
        System.out.println("POST Response Code :: " + responseCode);

        // exit conditions
        if (responseCode != HttpURLConnection.HTTP_OK) {
            System.out.println("POST request not worked");
        }

        // read response
        BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
        String inputLine;
        StringBuilder response = new StringBuilder();

        while ((inputLine = in.readLine()) != null) {
            response.append(inputLine);
        }
        in.close();

        // print result
        System.out.println(response.toString());
    }

    private void get() throws IOException {
        // setup request
        HttpURLConnection con = (HttpURLConnection) new URL(GET_URL).openConnection();
        con.setRequestMethod("GET");
        con.setRequestProperty("User-Agent", USER_AGENT);
        con.setRequestProperty("Authorization", "Basic " + new String(Base64.getEncoder().encode(USER.getBytes())));
        con.setRequestProperty("Content-Type", "application/json");

        // get status code
        int responseCode = con.getResponseCode();
        System.out.println("GET Response Code :: " + responseCode);

        // exit conditions
        if (responseCode != HttpURLConnection.HTTP_OK) {
            System.out.println("GET request not worked");
        }

        // read response
        BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
        String inputLine;
        StringBuilder response = new StringBuilder();

        while ((inputLine = in.readLine()) != null) {
            response.append(inputLine);
        }
        in.close();

        // print result
        System.out.println(response.toString());
    }
}