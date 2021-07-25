package com.veniuscloudpower.kubedashboard.models.tasks;

import com.veniuscloudpower.kubedashboard.models.KubeBaseEntity;
import com.veniuscloudpower.kubedashboard.models.User;
import com.veniuscloudpower.kubedashboard.models.kb.KbPostItem;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDateTime;


@Entity
@Getter
@Setter
@Table(name = "TaskDiscussions")
public class TaskDiscussion extends KubeBaseEntity {


    @ManyToOne
    private Task taskItem;

    @Column(columnDefinition="TEXT")
    private String discussionText;

    @ManyToOne
    @JoinColumn(name="author_user_id", referencedColumnName="user_id")
    private User author;

    @Column(name = "dateCreated")
    private LocalDateTime dateCreated;

}
