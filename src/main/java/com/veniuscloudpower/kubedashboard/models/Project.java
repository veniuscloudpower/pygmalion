package com.veniuscloudpower.kubedashboard.models;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.io.Serializable;

@Entity
@Table(name = "projects")
@Getter
@Setter
public class Project implements Serializable {

    @Id
    @Column(name = "projects_id")
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    private String projectName;

    @ManyToOne
    @JoinColumn(name="organizations_id", referencedColumnName="organizations_id")
    private Organization organization;
}
