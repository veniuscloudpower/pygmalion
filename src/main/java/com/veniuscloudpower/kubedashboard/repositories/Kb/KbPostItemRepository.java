package com.veniuscloudpower.kubedashboard.repositories.Kb;

import com.veniuscloudpower.kubedashboard.models.kb.KbPostItem;
import org.springframework.data.jpa.repository.JpaRepository;

public interface KbPostItemRepository extends JpaRepository<KbPostItem,Integer> {
}
