package com.veniuscloudpower.kubedashboard;

import com.veniuscloudpower.kubedashboard.models.Organization;
import com.veniuscloudpower.kubedashboard.models.User;
import com.veniuscloudpower.kubedashboard.repositories.OrganizationRepository;
import com.veniuscloudpower.kubedashboard.repositories.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

@Component
public class InitialSetup implements CommandLineRunner {

    final
    UserRepository userRepository;

    final
    OrganizationRepository organizationRepository;


    final
    WebSecurityConfig webSecurityConfig;

    @Autowired
    public InitialSetup(UserRepository userRepository, OrganizationRepository organizationRepository, WebSecurityConfig webSecurityConfig) {
        this.userRepository = userRepository;
        this.webSecurityConfig = webSecurityConfig;
        this.organizationRepository= organizationRepository;
    }

    @Override
    public void run(String... args) throws Exception {
        if (userRepository.count()==0)
        {
            var organization = new Organization();
            organization.setName("Default");
            organizationRepository.save(organization);


            var initialUser  = new User("user","user","user");

            initialUser.setPassword(webSecurityConfig.passwordEncoder().encode("user"));

            initialUser.setOrganizations(organization);
            initialUser.setRole("admin");
            initialUser.setEnabled(true);

            userRepository.save(initialUser);



        }
    }
}
