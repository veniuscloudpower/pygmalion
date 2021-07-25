package com.veniuscloudpower.kubedashboard.models.tasks;

import com.veniuscloudpower.kubedashboard.models.KubeBaseEntity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.Table;


@Entity
@Getter
@Setter
@Table(name = "TaskCategories")
public class TaskCategory extends KubeBaseEntity {

    private  String categoryName;
}
