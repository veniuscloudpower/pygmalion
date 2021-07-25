package com.veniuscloudpower.kubedashboard.models;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.io.Serializable;


@Entity
@Table(name = "users")
@Getter
@Setter
public class User implements Serializable {

    public User(String userName , String firstName,String lastName)
    {
        this.enabled = true;
        this.userName = userName;
        this.firstName = firstName;
        this.lastName  = lastName;
        this.avatar = "/img/avatar.png";
        this.role = "admin";
    }

    @Id
    @Column(name = "user_id")
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    @Column(name = "username")
    private String userName;
    private String password;


    private String role;


    private boolean enabled;

    @Column(columnDefinition = "text")
    private  String avatar;

    private  String firstName;
    private  String lastName;

    private  String email;
    private  String mobile;

    @ManyToOne
    @JoinColumn(name="organizations_id", referencedColumnName="organizations_id")
    private Organization organizations;


    public User() {
        // empty contractor without setting default values
    }
}

