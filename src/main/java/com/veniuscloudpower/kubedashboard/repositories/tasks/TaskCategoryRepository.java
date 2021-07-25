package com.veniuscloudpower.kubedashboard.repositories.tasks;

import com.veniuscloudpower.kubedashboard.models.tasks.TaskCategory;
import org.springframework.data.jpa.repository.JpaRepository;

public interface TaskCategoryRepository extends JpaRepository<TaskCategory,Integer> {
}
