package com.veniuscloudpower.kubedashboard.repositories;

import com.veniuscloudpower.kubedashboard.models.Project;
import org.springframework.data.jpa.repository.JpaRepository;


public interface ProjectRepository extends JpaRepository<Project, Integer> {
}
