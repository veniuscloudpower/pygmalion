package com.veniuscloudpower.kubedashboard.models;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;


@Getter
@Setter
@MappedSuperclass
public class KubeBaseEntity {

    @Id
    @GeneratedValue(strategy= GenerationType.IDENTITY)
    private  int id;

    @ManyToOne
    @JoinColumn(name="organizations_id", referencedColumnName="organizations_id")
    private Organization organization;

    @ManyToOne
    @JoinColumn(name="projects_id", referencedColumnName="projects_id")
    private Project project;
}
