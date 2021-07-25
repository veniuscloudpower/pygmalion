package com.veniuscloudpower.kubedashboard.models;


import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.*;
import java.io.Serializable;

@Entity
@Getter
@Setter
@Table(name="organizations")
public class Organization implements Serializable {

    @Id
    @Column(name = "organizations_id")
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    private String name;
}
