package com.veniuscloudpower.kubedashboard.payloads.response;

import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
public class JwtResponse {

    public JwtResponse(String token, int id, String username, String email, List<String> roles)
    {
        this.id = id;
        this.token=token;
        this.username = username;
        this.email=email;
        this.roles=roles;
    }

    private String token;
    private String type;
    private int id;
    private String username;
    private String email;
    private List<String> roles;
}
