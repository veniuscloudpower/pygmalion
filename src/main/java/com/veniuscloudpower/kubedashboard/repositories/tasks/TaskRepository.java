package com.veniuscloudpower.kubedashboard.repositories.tasks;

import com.veniuscloudpower.kubedashboard.models.tasks.Task;
import org.springframework.data.jpa.repository.JpaRepository;

public interface TaskRepository extends JpaRepository<Task,Integer> {
}
