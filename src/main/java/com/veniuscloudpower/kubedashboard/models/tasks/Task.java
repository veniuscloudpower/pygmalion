package com.veniuscloudpower.kubedashboard.models.tasks;

import com.veniuscloudpower.kubedashboard.models.KubeBaseEntity;
import com.veniuscloudpower.kubedashboard.models.User;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;


@Entity
@Getter
@Setter
@Table(name = "Tasks")
public class Task extends KubeBaseEntity {

    public Task()
    {
        discussion = new ArrayList<>();
    }

    @ManyToOne
    @JoinColumn(name="assigned_user_id", referencedColumnName="user_id")
    private User assignedUser;


    @Column(name = "dateCreated")
    private LocalDateTime dateCreated;

    @ManyToOne
    @JoinColumn(name="creator_user_id", referencedColumnName="user_id")
    private User creator;

    private String title;

    private String description;

    @Column(name = "dateDue",nullable = true)
    private LocalDateTime dateDue;

    @Enumerated(EnumType.ORDINAL)
    private TaskStatus taskStatus;

    @OneToMany(mappedBy = "taskItem")
    private List<TaskDiscussion> discussion;

}
