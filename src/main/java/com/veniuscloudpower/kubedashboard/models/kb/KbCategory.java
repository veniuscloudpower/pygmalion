package com.veniuscloudpower.kubedashboard.models.kb;

import com.veniuscloudpower.kubedashboard.models.KubeBaseEntity;
import com.veniuscloudpower.kubedashboard.models.Organization;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.io.Serializable;


@Getter
@Setter
@Entity
@Table(name="KbCategories")
public class KbCategory extends KubeBaseEntity implements Serializable {

    private  String categoryName;

    private  String categoryDescription;

    private  Boolean hasArticles;

}
