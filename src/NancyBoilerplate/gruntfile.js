/**
 * Unfamiliar with Grunt? Here are some links that will allow you to learn more.
 *
 * - http://gruntjs.com
 * - http://gruntjs.com/getting-started
 * - http://howtonode.org/introduction-to-npm
 */

'use strict';

module.exports = function(grunt) {

    /**
     * Configuring the tasks available for the build process.
     */
    grunt.initConfig({

        /**
         * Compiles Sass into a CSS file.
         */
        sass: {
            dev: {
                files: {
                    'assets/css/styles.css': 'assets/css/styles.scss'
                }
            },

            /**
             * Creates a compressed version of the project style sheet.
             */
            dist: {
                options: {
                    style: 'compressed'
                },
                files: {
                    'assets/css/styles.css': 'assets/css/styles.scss'
                }
            }
        }
    });

    /**
     * Loads libraries to assist with the build process.
     */
    grunt.loadNpmTasks('grunt-contrib-sass');

    /**
     * Organises the front end assets ready for a production environment.
     */
    grunt.registerTask('dist', ['sass:dist']);
};
