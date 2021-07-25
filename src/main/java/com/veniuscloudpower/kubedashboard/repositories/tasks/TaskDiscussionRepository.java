package com.veniuscloudpower.kubedashboard.repositories.tasks;

import com.veniuscloudpower.kubedashboard.models.tasks.TaskDiscussion;
import org.springframework.data.jpa.repository.JpaRepository;

public interface TaskDiscussionRepository extends JpaRepository<TaskDiscussion,Integer> {
}
