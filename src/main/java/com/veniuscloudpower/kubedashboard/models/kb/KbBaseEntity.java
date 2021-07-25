package com.veniuscloudpower.kubedashboard.models.kb;



import com.veniuscloudpower.kubedashboard.models.KubeBaseEntity;
import com.veniuscloudpower.kubedashboard.models.User;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Column;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.MappedSuperclass;
import java.time.LocalDateTime;

@Getter
@Setter
@MappedSuperclass
public class KbBaseEntity extends KubeBaseEntity {

    @ManyToOne
    @JoinColumn(name="author_user_id", referencedColumnName="user_id")
    private User author;

    @Column(name = "dateCreated")
    private LocalDateTime dateCreated;
}
