package com.veniuscloudpower.kubedashboard.repositories;

import com.veniuscloudpower.kubedashboard.models.Organization;
import org.springframework.data.jpa.repository.JpaRepository;

public interface OrganizationRepository extends JpaRepository<Organization, Integer> {
}
