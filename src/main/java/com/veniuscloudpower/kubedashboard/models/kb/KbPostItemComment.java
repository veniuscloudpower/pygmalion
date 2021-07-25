package com.veniuscloudpower.kubedashboard.models.kb;

import com.veniuscloudpower.kubedashboard.models.KubeBaseEntity;
import com.veniuscloudpower.kubedashboard.models.User;
import lombok.Getter;
import lombok.Setter;


import javax.persistence.*;
import java.time.LocalDateTime;


@Getter
@Setter
@Entity
@Table(name = "KbPostItemComments")
public class KbPostItemComment extends KbBaseEntity {

    @ManyToOne
    private KbPostItem kbItem;

    @Column(columnDefinition="TEXT")
    private String commentText;

}


