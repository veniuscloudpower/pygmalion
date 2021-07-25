package com.veniuscloudpower.kubedashboard.repositories.Kb;

import com.veniuscloudpower.kubedashboard.models.kb.KbCategory;
import org.springframework.data.jpa.repository.JpaRepository;


public interface KbCategoryRepository extends JpaRepository<KbCategory, Integer> {
}
