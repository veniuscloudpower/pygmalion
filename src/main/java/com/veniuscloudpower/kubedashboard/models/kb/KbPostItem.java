package com.veniuscloudpower.kubedashboard.models.kb;

import com.veniuscloudpower.kubedashboard.models.KubeBaseEntity;
import com.veniuscloudpower.kubedashboard.models.User;
import lombok.Getter;
import lombok.Setter;


import javax.persistence.*;
import java.io.Serializable;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Entity
@Getter
@Setter
@Table(name="KbPostItems")
public class KbPostItem extends KbBaseEntity implements Serializable {

    public  KbPostItem()
    {
        comments = new ArrayList<>();
    }

    @ManyToOne
    @JoinColumn(name="category_id", referencedColumnName="id")
    private KbCategory category;

    @Column(length = 150)
    private String  title;

    @Column(columnDefinition="TEXT")
    private  String description;

    @Column(length = 1500)
    private  String website;

    private  Integer avgRate;

    @OneToMany(mappedBy = "kbItem")
    private List<KbPostItemComment> comments;


}
