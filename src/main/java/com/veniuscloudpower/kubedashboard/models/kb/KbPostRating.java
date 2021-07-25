package com.veniuscloudpower.kubedashboard.models.kb;

import com.veniuscloudpower.kubedashboard.models.KubeBaseEntity;
import com.veniuscloudpower.kubedashboard.models.User;
import lombok.Getter;
import lombok.Setter;


import javax.persistence.*;
import java.io.Serializable;


@Entity
@Getter
@Setter
@Table(name = "KbPostRatings")
public class KbPostRating extends KubeBaseEntity implements Serializable {

    @ManyToOne
    private KbPostItem kbItem;

    @ManyToOne
    private User rateBy;

    private  Integer rate;
}

