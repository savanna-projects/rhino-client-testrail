package com.automation.gurock.contracts;

public class TestRailHttpCommand {
    private String httpMethod;
    private String endpoint;
    private String contentType;
    private Object data;

    /**
     * Gets the HTTP method to use with this command
     *
     * @return HTTP method to use with this command
     */
    public String getHttpMethod() {
        return httpMethod == null || httpMethod.equals("") ? "GET" : httpMethod;
    }

    /**
     * Sets the HTTP method to use with this command
     *
     * @param httpMethod HTTP method to use with this command
     * @return Self reference
     */
    public TestRailHttpCommand setHttpMethod(String httpMethod) {
        this.httpMethod = httpMethod;
        return this;
    }

    /**
     * Gets the HTTP command endpoint, including routing, not including server base URL
     *
     * @return HTTP command endpoint, including routing, not including server base URL
     */
    public String getEndpoint() {
        return endpoint;
    }

    /**
     * Sets the HTTP command endpoint, including routing, not including server base URL
     *
     * @param endpoint HTTP command endpoint, including routing, not including server base URL
     * @return Self reference
     */
    public TestRailHttpCommand setEndpoint(String endpoint) {
        this.endpoint = endpoint;
        return this;
    }

    /**
     * Gets the command content type
     *
     * @return command content type
     */
    public String getContentType() {
        return contentType == null || contentType.equals("") ? "application/json" : contentType;
    }

    /**
     * Sets this command content type
     *
     * @param contentType command content type
     * @return Self reference
     */
    public TestRailHttpCommand setContentType(String contentType) {
        this.contentType = contentType;
        return this;
    }

    /**
     * Gets the data object to send with this command (request body)
     *
     * @return data object to send with this command (request body)
     */
    public Object getData() {
        return data;
    }

    /**
     * Sets the data object to send with this command (request body)
     *
     * @param data data object to send with this command (request body)
     * @return Self reference
     */
    public TestRailHttpCommand setData(Object data) {
        this.data = data;
        return this;
    }
}